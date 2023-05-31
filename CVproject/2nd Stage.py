import itertools
import time

import cv2
import numpy as np
from glob import glob
import argparse
import tflearn
from matplotlib import pyplot as plt
import os
import pandas as pd
from sklearn.cluster import KMeans
from sklearn.svm import LinearSVC as SVC
from sklearn.preprocessing import StandardScaler
from random import shuffle
from sklearn.metrics import confusion_matrix, plot_confusion_matrix

#Reading Training Data
Train_PersonA = pd.read_csv("personA_SigVerificationTrainLabels.csv")
Test_PersonA = pd.read_csv("personA_SigVerificationTestLabels.csv")

Train_PersonB = pd.read_csv("personB_SigVerificationTrainLabels.csv")
Test_PersonB = pd.read_csv("personB_SigVerificationTestLabels.csv")

Train_PersonC = pd.read_csv("personC_SigVerificationTrainLabels.csv")
Test_PersonC = pd.read_csv("personC_SigVerificationTestLabels.csv")

Train_PersonD = pd.read_csv("personD_SigVerificationTrainLabels.csv")
Test_PersonD = pd.read_csv("personD_SigVerificationTestLabels.csv")

Train_PersonE = pd.read_csv("personE_SigVerificationTrainLabels.csv")
Test_PersonE = pd.read_csv("personE_SigVerificationTestLabels.csv")

def plot_confusion_matrix(cm, classes,
                        normalize=False,
                        title='Confusion matrix',
                        cmap=plt.cm.Blues):
    """
    This function prints and plots the confusion matrix.
    Normalization can be applied by setting `normalize=True`.
    """
    plt.imshow(cm, interpolation='nearest', cmap=cmap)
    plt.title(title)
    plt.colorbar()
    tick_marks = np.arange(len(classes))
    plt.xticks(tick_marks, classes, rotation=45)
    plt.yticks(tick_marks, classes)

    if normalize:
        cm = cm.astype('float') / cm.sum(axis=1)[:, np.newaxis]
        print("Normalized confusion matrix")
    else:
        print('Confusion matrix, without normalization')

    print(cm)

    thresh = cm.max() / 2.
    for i, j in itertools.product(range(cm.shape[0]), range(cm.shape[1])):
        plt.text(j, i, cm[i, j],
            horizontalalignment="center",
            color="white" if cm[i, j] > thresh else "black")

    plt.tight_layout()
    plt.ylabel('True label')
    plt.xlabel('Predicted label')

def Second_Stage_Model(Df , Df2 ):
    Train_data = Df["image_name"]
    Train_lables = Df["label"]
    Training_Dataset = []
    Train_dir = "Train"
    images_count = len(Train_data)
    Train_imgs = []
    train_lbls = np.array([])

    sift =  cv2.xfeatures2d.SIFT_create()
    Descriptor_list = []
    n_clusters = 10
    K_mean_obj = KMeans(n_clusters=n_clusters)

    for i in range(images_count):
        Training_Dataset.append([Train_data[i] , Train_lables[i]])

    shuffle(Training_Dataset)

    for i in Training_Dataset:
        if i[1] == "forged":
            train_lbls = np.append(train_lbls , 0)
        elif i[1] == "real":
            train_lbls = np.append(train_lbls , 1)


    for img in Training_Dataset:
        path = os.path.join(Train_dir , img[0])
        image = cv2.imread(path , 0)
        Train_imgs.append([image , img[1]])
        kp , des = sift.detectAndCompute(image, None)
        Descriptor_list.append(des)


    vStack = np.array(Descriptor_list[0])
    for remaining in Descriptor_list[1:]:
        vStack = np.vstack((vStack, remaining))
    descriptor_vstack = vStack.copy()


    #Training
    starttrain = time.time()
    K_mean = K_mean_obj.fit_predict(descriptor_vstack)

    mega_histogram = np.array([np.zeros(n_clusters) for i in range(images_count)])


    #Filling Histogram Of each image
    counter = 0
    for i in range(images_count):
        l = len(Descriptor_list[i])
        for j in range(l):
            idx = K_mean[counter + j]
            mega_histogram[i][idx] += 1
        counter += l

    #Histogram Visualization

    vocabul = mega_histogram

    x_scalar = np.arange(n_clusters)
    y_scalar = np.array([abs(np.sum(vocabul[:, h], dtype=np.int32)) for h in range(n_clusters)])

    print(y_scalar)

    plt.bar(x_scalar, y_scalar)
    plt.xlabel("Visual Word Index")
    plt.ylabel("Frequency")
    plt.title("Complete Vocabulary Generated")
    plt.xticks(x_scalar + 0.4, x_scalar)
    plt.show()

    #Standardize
    scale = StandardScaler().fit(mega_histogram)
    mega_histogram = scale.transform(mega_histogram)


    #SVM model Training
    clf = SVC()
    clf.fit(mega_histogram, train_lbls)
    endtrain = time.time()
    Traintime = endtrain - starttrain

    #Training Accuracy
    indx = 0
    TrueTrainValues = 0
    for images in Train_imgs:
        Train_kp, Train_des = sift.detectAndCompute(images[0], None)
        vocab = np.array([[0 for i in range(n_clusters)]])
        vocab = np.array(vocab, 'float32')
        train_ret = K_mean_obj.predict(Train_des)
        for each in train_ret:
            vocab[0][each] +=1
        vocab = scale.transform(vocab)
        T_class_predicted = clf.predict(vocab)

        if T_class_predicted == 0:
            object = "forged"
        else:
            object = "real"

        if (object == images[1]):
            TrueTrainValues +=  1
        indx+=1

    TrainAccuracy = (TrueTrainValues/len(Train_imgs))*100



    #Testing
    y_test = []
    y_predict = []
    start = time.time()
    Test_Dir = "Test"
    Test_CSV = Df2
    Test_imgs = Test_CSV["image_name"]
    Test_Labels = Test_CSV["label"]
    Test_images =[]
    Test_imgs_count = len(Test_imgs)
    predictions = []
    index =0
    TrueValues =0
    Test_Dataset = []

    for i in range(len(Test_imgs)):
        Test_Dataset.append([Test_imgs[i] , Test_Labels[i]])

    shuffle(Test_Dataset)


    for im in Test_Dataset:
        path = os.path.join(Test_Dir , im[0])
        tst_img = cv2.imread(path,0)
        Test_images.append(tst_img)

        Test_kp, Test_des = sift.detectAndCompute(tst_img, None)
        vocablary = np.array([[0 for i in range(n_clusters)]])
        vocablary = np.array(vocablary, 'float32')
        test_ret = K_mean_obj.predict(Test_des)
        for each in test_ret:
            vocablary[0][each] +=1

        vocablary = scale.transform(vocablary)
        class_predicted = clf.predict(vocablary)

        if class_predicted == 0:
            object = "forged"

        else:
            object = "real"


        if im[1] == "forged":
            y_test.append(0)
        else:
            y_test.append(1)
        y_predict.append(class_predicted)



        if (object == im[1]):
            TrueValues +=  1
        index+=1

    end = time.time()
    Testtime = end-start
    TestingAccuracy = (TrueValues/len(Test_imgs))*100

    #Confusion matrix
    cm = confusion_matrix(y_true = y_test, y_pred =y_predict)
    plot_confusion_matrix(cm = cm , classes = ["forged" , "real"] , title = "Confusion Matrix")
    plt.show()


    #Test Script
    Script_dir = "SignatureTestSamples"
    Script_imgs = os.listdir(Script_dir)
    name = Train_data[1].split("_")
    name = name[0]
    if name == "personA":
        for imag in range(2):
            path = os.path.join(Script_dir , Script_imgs[imag])
            image1 = cv2.imread(path , 0)
            Test_kp, Test_des = sift.detectAndCompute(image1, None)
            vocabla = np.array([[0 for i in range(n_clusters)]])
            vocabla = np.array(vocabla, 'float32')
            test_return = K_mean_obj.predict(Test_des)
            for each in test_return:
                vocabla[0][each] += 1

            vocabla = scale.transform(vocabla)
            class_predict = clf.predict(vocabla)
            if class_predict == 0:
                object1 = "forged"

            else:
                object1 = "real"

            Y_actual = Script_imgs[imag].split("_")
            Y_actual = Y_actual[1].split(".")
            Y_actual = Y_actual[0]

            if Y_actual == "real2":
                Y_actual="real"

            if (object1 == Y_actual):
                result = "True prediction : "+object1
            else:
                result = "False Prediction : " + object1

            plt.imshow(image1)
            plt.text(-50, -10, result, fontsize=15)

            plt.show()

    elif  name == "personD":
        for imag in range(2, 5):
            path = os.path.join(Script_dir , Script_imgs[imag])
            image1 = cv2.imread(path , 0)
            Test_kp, Test_des = sift.detectAndCompute(image1, None)
            vocabla = np.array([[0 for i in range(n_clusters)]])
            vocabla = np.array(vocabla, 'float32')
            test_return = K_mean_obj.predict(Test_des)
            for each in test_return:
                vocabla[0][each] += 1

            vocabla = scale.transform(vocabla)
            class_predict = clf.predict(vocabla)
            if class_predict == 0:
                object1 = "forged"

            else:
                object1 = "real"

            Y_actual = Script_imgs[imag].split("_")
            Y_actual = Y_actual[1].split(".")
            Y_actual = Y_actual[0]

            if Y_actual == "real2":
                Y_actual="real"

            if (object1 == Y_actual):
                result = "True prediction : "+object1
            else:
                result = "False Prediction : " + object1

            plt.imshow(image1)
            plt.text(-50, -10, result, fontsize=15)

            plt.show()

    return TrainAccuracy , TestingAccuracy , Traintime , Testtime




#Printing Accuracies
Train_Acc_A , Test_Acc_A , TrainTimeA , TestTimeA =Second_Stage_Model(Train_PersonA , Test_PersonA)
Train_Acc_B , Test_Acc_B , TrainTimeB , TestTimeB =Second_Stage_Model(Train_PersonB , Test_PersonB)
Train_Acc_C , Test_Acc_C ,TrainTimeC , TestTimeC =Second_Stage_Model(Train_PersonC , Test_PersonC)
Train_Acc_D , Test_Acc_D , TrainTimeD , TestTimeD =Second_Stage_Model(Train_PersonD , Test_PersonD)
Train_Acc_E , Test_Acc_E , TrainTimeE , TestTimeE =Second_Stage_Model(Train_PersonE , Test_PersonE)

print("Train Accuracy Of Person A = " , Train_Acc_A )
print("Train Time Of Person A = " , TrainTimeA)
print("Test Accuracy Of Person A = " , Test_Acc_A )
print("Test Time Of Person A = " , TestTimeA)

print("____________________________")

print("Train Accuracy Of Person B = " , Train_Acc_B )
print("Train Time Of Person B = " , TrainTimeB)
print("Test Accuracy Of Person B = " , Test_Acc_B )
print("Test Time Of Person B = " , TestTimeB)

print("____________________________")

print("Train Accuracy Of Person C = " , Train_Acc_C )
print("Train Time Of Person C = " , TrainTimeC)
print("Test Accuracy Of Person C = " , Test_Acc_C )
print("Test Time Of Person C = " , TestTimeC)

print("____________________________")

print("Train Accuracy Of Person D = " , Train_Acc_D )
print("Train Time Of Person D = " , TrainTimeD)
print("Test Accuracy Of Person D = " , Test_Acc_D )
print("Test Time Of Person D = " , TestTimeD)

print("____________________________")

print("Train Accuracy Of Person E = " , Train_Acc_E )
print("Train Time Of Person E = " , TrainTimeE)
print("Test Accuracy Of Person E = " , Test_Acc_E )
print("Test Time Of Person E = " , TestTimeE)

