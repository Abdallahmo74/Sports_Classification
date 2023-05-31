import itertools
import os
import cv2
import pandas as pd
import tensorflow as tf
import numpy as np
import tflearn
import seaborn as sns
from sklearn.cluster import KMeans
from sklearn.metrics import confusion_matrix
from sklearn.preprocessing import StandardScaler
from tflearn.layers.core import input_data , dropout , fully_connected
from tflearn.layers.conv import conv_2d , max_pool_2d
from random import shuffle
from tflearn.layers.estimator import regression
import matplotlib.pyplot as plt
import time
from sklearn.svm import LinearSVC as SVC

#Data preparation
IMG_SIZE = 500

def Create_Lables(img_name):
    Lable = img_name.split('_')
    Lable = Lable[0]
    if Lable == "personA":
        return np.array([1 , 0 , 0 , 0 , 0])
    elif Lable == "personB":
        return np.array([0 , 1 , 0 , 0 , 0])
    elif Lable == "personC":
        return np.array([0 , 0 , 1 , 0 , 0])
    elif Lable == "personD":
        return np.array([0 , 0 , 0 , 1 , 0])
    elif Lable == "personE":
        return np.array([0 , 0 , 0 , 0 , 1])



def Train_Data():
    Train_Dir = 'Train'
    Train_Files = os.listdir(Train_Dir)
    Training_Dataset = []
    dataset = np.ndarray(shape=(len(Train_Files), IMG_SIZE, IMG_SIZE , 1), dtype=np.uint8)
    ind = 0
    Lables = []
    for img in Train_Files:
        path = os.path.join(Train_Dir , img)
        image = cv2.imread(path , 0)
        image = cv2.resize(image , (IMG_SIZE,IMG_SIZE))
        image = cv2.normalize(image, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_32F)
        Training_Dataset.append([np.array(image) , Create_Lables(img)])
        dataset[ind] = (np.array(image).reshape(IMG_SIZE,IMG_SIZE,1))
        Lables.append(Create_Lables(img))
        ind+=1


    #Data Augmentation (Rotating Photos by 90 degrees)
    indx = 0
    sess = tf.compat.v1.Session()
    for img in dataset:

        Augmented_Img = tf.image.rot90(img)
        im = sess.run(Augmented_Img)
        Training_Dataset.append([np.array(im) , Lables[indx]])
        indx +=1

    shuffle(Training_Dataset)
    sess.close()
    np.save('Train_Data.npy', Training_Dataset)
    return Training_Dataset




#Test Dataset
def Test_Data():
    Test_Dir = "Test"
    Test_Files = os.listdir(Test_Dir)
    Test_dataset =[]
    for img in  Test_Files:
        path = os.path.join(Test_Dir , img)
        image = cv2.imread(path,0)
        image = cv2.resize(image , (IMG_SIZE,IMG_SIZE))
        image = cv2.normalize(image, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_32F)
        Test_dataset.append([np.array(image) , Create_Lables(img)])

    shuffle(Test_dataset)
    np.save('Test_Data.npy', Test_dataset)
    return Test_dataset


#Train Data Check
#If The file exists dont run Train data again and use the data in the file
if (os.path.exists('Train_Data.npy')):
    train_data =np.load('Train_Data.npy',allow_pickle=True)

else:
    train_data = Train_Data()



#Test Data Check
#If The file exists dont run Test data again and use the data in the file
if (os.path.exists('Test_Data.npy')):
    test_data =np.load('Test_Data.npy',allow_pickle=True)

else:
    test_data = Test_Data()


LR = 0.001
MODEL_NAME = 'Handwritten_CNN'
Train = train_data[0:300]
Validation = train_data[300:]

X_train=[]
for i in Train :
    X_train.append(np.array(i[0]).reshape(IMG_SIZE , IMG_SIZE,1))
X_train= np.array(X_train)
Y_train = [i[1] for i in Train]

X_Valid = []
for i in Validation:
    X_Valid.append(np.array(i[0]).reshape(IMG_SIZE, IMG_SIZE, 1))
X_Valid = np.array(X_Valid)
Y_Valid = [i[1] for i in Validation]

tf.compat.v1.reset_default_graph()

#CNN MODEL

conv_input = input_data(shape=[None, IMG_SIZE, IMG_SIZE, 1], name='input')

conv1 = conv_2d(conv_input, 32, 5, activation='relu')
pool1 = max_pool_2d(conv1, 5)

conv2 = conv_2d(pool1, 64, 5, activation='relu')
pool2 = max_pool_2d(conv2, 5)

conv3 = conv_2d(pool2, 32, 5, activation='relu')
pool3 = max_pool_2d(conv3, 5)

fully_connect = fully_connected(pool3, 1024, activation='relu')
fully_connect = dropout(fully_connect , 0.5)

cnn_layers = fully_connected(fully_connect, 5 , activation='softmax')
cnn_layers = regression(cnn_layers, optimizer='adam', learning_rate=LR, loss='categorical_crossentropy', name='targets')

Model = tflearn.DNN(cnn_layers, tensorboard_dir='log', tensorboard_verbose=3)


if (os.path.exists('Model.tfl.meta')):
    Model.load('./Model.tfl')

else:
    Model.fit({'input': X_train}, {'targets': Y_train}, n_epoch=40,
           validation_set=({'input': X_Valid}, {'targets': Y_Valid}),
           snapshot_step= 500, show_metric=True, run_id= MODEL_NAME)
    Model.save('Model.tfl')



#Calculating Training Accuracy
Train_Count = len(train_data)
Train_True_Values = 0
X_Train_A = []
for i in train_data:
    X_Train_A.append(np.array(i[0].reshape(IMG_SIZE,IMG_SIZE,1)))
X_Train_A = np.array(X_Train_A)
Y_Train_A = [i[1] for i in train_data]
indx = 0
for img in X_Train_A:

    prediction = Model.predict([img])[0]
    predicted_value = prediction.argmax()
    if Y_Train_A[indx][predicted_value] == 1:
        Train_True_Values+=1
    indx+=1

Train_Accuracy = (Train_True_Values/Train_Count)*100
print("Train_Acc = " , Train_Accuracy)


#Calculating Testing Accuracy
start = time.time()
Y_tst = []
Y_predict = []
Test_count = len(test_data)
true_values = 0
X_test = []
for i in test_data :
    X_test.append(np.array(i[0]).reshape(IMG_SIZE , IMG_SIZE,1))
X_test= np.array(X_test)

Y_test = [i[1] for i in test_data]
index = 0

for img in X_test:

    prediction = Model.predict([img])[0]
    predicted_value = prediction.argmax()
    Y_predict.append(predicted_value)
    Y_tst.append(Y_test[index])
    if Y_test[index][predicted_value] == 1:
        true_values+=1
    index+=1
end = time.time()

Test_Accuracy = (true_values/Test_count)*100
print("Test_Acc = " , Test_Accuracy)
print("Testing Time = " , end - start)

Y_Actual = []
for x in Y_tst:
    Y_Actual.append(x.argmax())




#Test Script
Script_dir = "SignatureTestSamples"
imag = os.listdir(Script_dir)
for imm in imag:
    path = os.path.join(Script_dir,imm)
    image1 = cv2.imread(path , 0)
    image1 =cv2.resize(image1 , (IMG_SIZE,IMG_SIZE))
    image1 = cv2.normalize(image1, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_32F)
    image1 = np.array(image1).reshape(IMG_SIZE , IMG_SIZE , 1)
    prediction_cls = Model.predict([image1])[0]
    predicted_value1 = prediction_cls.argmax()
    if predicted_value1 == 0:
        cls = "personA"
    elif predicted_value1 == 1:
        cls = "personB"
    elif predicted_value1 == 2:
        cls = "personC"
    elif predicted_value1 == 3:
        cls = "personD"
    elif predicted_value1 == 4:
        cls = "personE"

    prediction1 = imm.split("_")
    prediction1 = prediction1[0]



    if prediction1 == cls:
        result = "True Prediction : " + cls
    else:
        result = "False Prediction "


    plt.imshow(image1)
    plt.text(-50, -10, result, fontsize=15)
    plt.show()


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


def Second_Stage_Model(Df , Df2  ):
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

