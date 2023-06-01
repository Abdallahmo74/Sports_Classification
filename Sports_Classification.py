import tensorflow as tf
import numpy as np
import os
from random import shuffle
import matplotlib.pyplot as plt
import tflearn
from tflearn.layers.conv import conv_2d, max_pool_2d
from tflearn.layers.core import input_data, dropout, fully_connected , flatten
from tflearn.layers.estimator import regression
import cv2
import csv
import seaborn as sns
from sklearn.metrics import confusion_matrix
import pandas as pd
#Training Data Preparation
def Create_Lable(img_name):
    Lable = img_name.split("_")
    if Lable[0] == "Basketball":
        return np.array([1, 0 , 0 , 0 , 0 , 0])
    elif Lable[0] == "Football":
        return np.array([0, 1, 0, 0, 0, 0])
    elif Lable[0] == "Rowing":
        return np.array([0, 0, 1, 0, 0, 0])
    elif Lable[0] == "Swimming":
        return np.array([0, 0, 0, 1, 0, 0])
    elif Lable[0] == "Tennis":
        return np.array([0, 0, 0, 0, 1, 0])
    elif Lable[0] == "Yoga":
        return np.array([0, 0, 0, 0, 0, 1])




IMG_SIZE = 277
def Train_Data():


    data_dir= "Train\\"
    Train_Images = os.listdir(data_dir)

    Football_Images = []
    Basketball_Images = []
    Yoga_Images = []
    Swimming_Images = []
    Rowing_Images = []
    Tennis_Images = []

    for word in Train_Images:
        path = os.path.join(data_dir , word)
        image = cv2.imread(path, 1)
        image = cv2.resize(image, (IMG_SIZE, IMG_SIZE))
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        image = cv2.normalize(image, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_32F)
        Sport = word.split("_")
        if Sport[0] == 'Football':
            Football_Images.append(image)
        elif Sport[0] == 'Swimming':
            Swimming_Images.append(image)
        elif Sport[0] =='Yoga':
            Yoga_Images.append(image)
        elif Sport[0] == 'Tennis':
            Tennis_Images.append(image)
        elif Sport[0] == 'Basketball':
            Basketball_Images.append(image)
        elif Sport[0] == 'Rowing':
            Rowing_Images.append(image)


    Maximum = len(Yoga_Images)
    Training_dataset = []
    All_Images=[]
    All_Images.append(Basketball_Images)
    All_Images.append(Football_Images)
    All_Images.append(Rowing_Images)
    All_Images.append(Swimming_Images)
    All_Images.append(Tennis_Images)
    All_Images.append(Yoga_Images)

    Class_num = []
    for num in All_Images:
        Class_num.append(len(num))
    sess = tf.compat.v1.Session()
    class_index =0

    for Class in All_Images:
        augmented = 0
        for img in Class:
            img = np.array(img).reshape(IMG_SIZE , IMG_SIZE , 3)
            if Class_num[class_index]*2 < Maximum :
                A_image1 = tf.image.rot90(img)
                A_image2 = tf.image.flip_left_right(img)
                All_Images[class_index].append(np.array(sess.run(A_image1)).reshape(IMG_SIZE,IMG_SIZE,3))
                All_Images[class_index].append(np.array(sess.run(A_image2)).reshape(IMG_SIZE,IMG_SIZE,3))
                augmented+=2
            else:
                A_image = tf.image.rot90(img)
                All_Images[class_index].append(np.array(sess.run(A_image)).reshape(IMG_SIZE,IMG_SIZE,3))
                augmented+=1

            if augmented+Class_num[class_index] >= Maximum :
                break
        class_index += 1

    Classe = np.array(["Basketball" , "Football" , "Rowing" , "Swimming" , "Tennis" , "Yoga"])
    Test = []
    index_of_class = 0

    for Classes in All_Images:
        indexs = 0
        for img in Classes:
            if indexs > 408:
                Test.append([np.array(img), Create_Lable(Classe[index_of_class])])
            else:
                Training_dataset.append([np.array(img), Create_Lable(Classe[index_of_class])])
            indexs+=1
        index_of_class+=1



    shuffle(Test)
    shuffle(Training_dataset)
    sess.close()
    np.save('Train_Data.npy', Training_dataset)
    np.save('Test_Data.npy' , Test)
    return Training_dataset , Test






#Train Data Check
#If The file exists dont run Train data again and use the data in the file
if (os.path.exists('Train_Data.npy')):
    train_data =np.load('Train_Data.npy',allow_pickle=True)
    Test_Data = np.load('Test_Data.npy' , allow_pickle = True)

else:
    train_data , Test_Data = Train_Data()




LR = 0.001
MODEL_NAME = 'Sports_Classification_CNN'
Train = train_data
Test = Test_Data

X_train=[]
for i in Train :
    X_train.append(np.array(i[0]).reshape(IMG_SIZE , IMG_SIZE,3))
X_train= np.array(X_train)
Y_train = [i[1] for i in Train]


X_test = []
for i in Test :
    X_test.append(np.array(i[0]).reshape(IMG_SIZE , IMG_SIZE,3))
X_test= np.array(X_test)

Y_test = [i[1] for i in Test]

tf.compat.v1.reset_default_graph()


#AlexNet MODLE
Convolution_input = input_data(shape=[None, IMG_SIZE, IMG_SIZE, 3] ,  name='input')

Convolution1 = conv_2d(Convolution_input , 96 , 11 , strides= 4 , padding= 'valid' , activation='relu' )
pool1 = max_pool_2d( Convolution1 , 3 , strides=(2,2) , padding= 'valid')

Convolution2 = conv_2d(pool1, 256 , 5, strides= 1 , padding= 'same', activation='relu' )
pool2 = max_pool_2d(Convolution2 , 3 , strides= 2 ,  padding= 'valid' )


Convolution3 = conv_2d(pool2, 384 , 3 , strides= 1,padding= 'same', activation='relu'  )


Convolution4 = conv_2d(Convolution3,  384 , 3 , strides= 1,padding= 'same', activation='relu' )


Convolution5 = conv_2d(Convolution4, 256 , 3 ,strides=1, padding='same', activation='relu' )
pool3 = max_pool_2d(Convolution5 , 3 , strides=(2, 2),  padding='valid')


fully_layer1 = fully_connected(pool3, 4096, activation='relu')
fully_layer2 = fully_connected(fully_layer1 , 4096, activation='relu')
fully_layer3 = fully_connected(fully_layer2 , 1000, activation='relu')

CNN_Output_Layer = fully_connected(fully_layer3, 6 , activation='softmax')
CNN_Output_Layer = regression(CNN_Output_Layer, optimizer='adam', learning_rate=LR, loss='categorical_crossentropy', name='targets')

Model = tflearn.DNN(CNN_Output_Layer, tensorboard_dir='log', tensorboard_verbose=3)

if (os.path.exists('Model.tfl.meta')):
    Model.load('./Model.tfl')

else:
    Model.fit({'input': X_train}, {'targets': Y_train}, n_epoch=40,
           validation_set=({'input': X_test}, {'targets': Y_test}),
           snapshot_step= 500, show_metric=True, run_id= MODEL_NAME)
    Model.save('Model.tfl')


TestDir = "Test"
Predictions=[]
ind = 1
for test_imgs in os.listdir(TestDir):
    path = os.path.join(TestDir , test_imgs)
    test_img = cv2.imread(path , 1)
    test_img = cv2.resize(test_img, (IMG_SIZE, IMG_SIZE))
    test_img = cv2.normalize(test_img , None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX, dtype=cv2.CV_32F)
    test_img = test_img.reshape(IMG_SIZE, IMG_SIZE, 3)
    test_img = cv2.cvtColor(test_img, cv2.COLOR_BGR2RGB)
    prediction = Model.predict([test_img])[0]
    max = 0
    index = np.array(prediction).argmax()
    Predictions.append([test_imgs , index])

header = ["image_name" , "label"]
file = open('Prediction.csv', 'w+', newline ='')
with file:
    write = csv.writer(file)
    write.writerow(header)
    write.writerows(Predictions)


