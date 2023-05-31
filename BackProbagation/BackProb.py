import math

import pandas as pd
import numpy as np
import GUI
import matplotlib.pyplot as plt

#GUI
Main_GUI = GUI.MyGUI()

#Input Data From GUI

Ln = Main_GUI.Text3.get()
Neurons = Main_GUI.Text2.get()
Neurons_list = [int(item) for item in Neurons.split(',') if item.isdigit()]
Layers = Main_GUI.Text1.get()
epochs = Main_GUI.Text4.get()
Activation = Main_GUI.Selected_Feature1.get()
Bais = Main_GUI.Bais.get()
Bais_List = []
for j in Neurons_list:
    B = np.random.rand(j , 1)
    Bais_List.append(B)
# for output Bais
B = np.random.rand(3,1)
Bais_List.append(B)
#Reading Folder
data = pd.read_csv('penguins.csv')


#Preprocessing
def Normaliz(data):
    data = (data - np.array(data).min()) / (np.array(data).max()-np.array(data).min())
    return data

#Filling Null Values with Previous values
data = data.fillna(method = 'pad')

#Converting Ordinal data to Numeric values
datamapping={
    "male":1,
    "female":2
}

datamapping2 ={
    "Adelie":0,
    "Gentoo":1,
    "Chinstrap":2
}

data['species'] = data['species'].map(datamapping2)
data['gender']= data['gender'].map(datamapping)

#Normalization
data["bill_length_mm"] = Normaliz(data["bill_length_mm"])
data["bill_depth_mm"] = Normaliz(data["bill_depth_mm"])
data["flipper_length_mm"] = Normaliz(data["flipper_length_mm"])
data["gender"] = Normaliz(data["gender"])
data["body_mass_g"] = Normaliz(data["body_mass_g"])

#For shuffeling data
AdelieData = data[0:50]
GentooData = data[50:100]
Chinstrap = data[100:150]

shuffled_AdelieData = AdelieData.sample(frac=1)
shuffled_GentooData = GentooData.sample(frac=1)
shuffled_Chinstrap = Chinstrap.sample(frac=1)

#Training Sample
Trainingset = shuffled_AdelieData[0:30]
Trainingset = Trainingset.append(shuffled_GentooData[0:30])
Trainingset = Trainingset.append(shuffled_Chinstrap[0:30])
Trainingset = Trainingset.sample(frac=1)
#Features
TrainingFeatures = np.array(Trainingset['bill_length_mm']).reshape(90,1)
TrainingFeature2 = np.array(Trainingset['bill_depth_mm']).reshape(90,1)
TrainingFeature3 = np.array(Trainingset['flipper_length_mm']).reshape(90,1)
TrainingFeature4 = np.array(Trainingset['gender']).reshape(90,1)
TrainingFeature5 = np.array(Trainingset['body_mass_g']).reshape(90,1)

TrainingFeatures = np.concatenate((TrainingFeatures , TrainingFeature2) , axis = 1  )
TrainingFeatures = np.concatenate((TrainingFeatures , TrainingFeature3) , axis = 1  )
TrainingFeatures = np.concatenate((TrainingFeatures , TrainingFeature4) , axis = 1  )
TrainingFeatures = np.concatenate((TrainingFeatures , TrainingFeature5) , axis = 1  )
TrainingLables1 = np.array(Trainingset['species']).reshape(90,1)
TrainingLables = np.zeros((90 , 3))

for i in range(90):
    if TrainingLables1[i,0] == 0:
        TrainingLables[i,0] = 1
    elif TrainingLables1[i,0] == 1:
        TrainingLables[i, 1] = 1
    elif TrainingLables1[i,0] == 2:
        TrainingLables[i,2] == 1

TrainingLables = TrainingLables.T


#Testing Sample
Testingset = shuffled_AdelieData[30:50]
Testingset = Testingset.append(shuffled_GentooData[30:50])
Testingset = Testingset.append(shuffled_Chinstrap[30:50])

TestFeature =  np.array(Testingset['bill_length_mm']).reshape(60,1)
TestFeature2 =  np.array(Testingset['bill_depth_mm']).reshape(60,1)
TestFeature3 =  np.array(Testingset['flipper_length_mm']).reshape(60,1)
TestFeature4 =  np.array(Testingset['gender']).reshape(60,1)
TestFeature5 =  np.array(Testingset['body_mass_g']).reshape(60,1)
TestFeature = np.concatenate((TestFeature , TestFeature2),axis=1)
TestFeature = np.concatenate((TestFeature , TestFeature3),axis=1)
TestFeature = np.concatenate((TestFeature , TestFeature4),axis=1)
TestFeature = np.concatenate((TestFeature , TestFeature5),axis=1)

TestFeature = TestFeature.T

TestingLables1 = np.array(Testingset['species']).reshape(60,1)
TestingLables = np.zeros((60 , 3))

for i in range(60):
    if TestingLables1[i,0] == 0:
        TestingLables[i,0] = 1
    elif TestingLables1[i,0] == 1:
        TestingLables[i, 1] = 1
    elif TestingLables1[i,0] == 2:
        TestingLables[i,2] = 1

TestingLables = TestingLables.T

def CalcuatingActivation(Activation , net ):
    Result = 0.0
    Results = []
    if Activation == "Sigmoid Function":
        for i in net:
            Result = 1 / (1 + (np.exp(-i)))
            Results.append(Result)
        Results = np.array(Results)

    else:
        for i in net:
            Result = (1 - (np.exp(-i))) / (1 + (np.exp(-i)))
            Results.append(Result)
        Results = np.array(Results)

    return Results

All_List = []
Back_All_List = []
inputweights = np.random.uniform(-1,1,size= (5, Neurons_list[0]))
All_List.append(inputweights)

#Weights Matrices initialization
for i in range(Layers):
    if i != (Layers-1):
        weights = np.random.uniform(-1 , 1 , size =(Neurons_list[i] , Neurons_list[i+1]))
        All_List.append(weights)
    else:
        weights = np.random.rand(Neurons_list[i] , 3)
        All_List.append(weights)

#if Bais is True Hidden have the values with Bais while NeuronsLayers Have values without Bais



#Transpose of Training Features
TrainingFeatures= TrainingFeatures.T




#Training
for e in range(epochs):
    for i in range (90):
        All_inputs=[]
        ind = 0
        indexOfList = 0
        HiddenLayersNeuronsActivation = []
        X = TrainingFeatures[:, i:i+1]
        All_inputs.append(X)
        for j in All_List:
            NeuronLayersTemp = []
            for y in range(j.shape[1]):
                Neu = 0
                F = X * j[ : , y : y+1]
                for z in range(len(F)):
                    Neu += F[z]
                NeuronLayersTemp.append(Neu)
            if Bais == "True":
                netValues = np.array(NeuronLayersTemp) + np.array(Bais_List[indexOfList])
            else:
                netValues = np.array(NeuronLayersTemp)
            Added = CalcuatingActivation(Activation , netValues )
            HiddenLayersNeuronsActivation.append(Added)
            All_inputs.append(Added)
            X = HiddenLayersNeuronsActivation[ind]
            ind+=1
            indexOfList+=1
        Predicted_Output = np.array(HiddenLayersNeuronsActivation[Layers])
        Actual_Output = np.array(TrainingLables[: , i]).reshape(3,1)
        Output_Error_Gradient = (Actual_Output - Predicted_Output)*(Predicted_Output)*((1-Predicted_Output))
        Back_All_List.append(Output_Error_Gradient)



        #BackPropagation
        for k in range(1,len(All_List)+1):
            Back = []
            for x in range(All_List[ind-k].shape[0]):
                sum = 0
                out = All_List[ind-k]
                multi = np.array(out[x:x+1 , : ]).T * np.array(Back_All_List[-1])
                for l in multi:
                    sum += l
                Back.append(sum)
            if k != len(All_List):
                Gradients = np.array(Back) * (np.array(HiddenLayersNeuronsActivation[Layers - k]) * (1-np.array(HiddenLayersNeuronsActivation[Layers - k])))
                Back_All_List.append(Gradients)



        #Updating Weights
        indx = 0
        for i in All_List:
            for n in range(i.shape[1]):
                i[ : , n:n+1] = i[ : , n:n+1] + (Ln * np.array(All_inputs[indx]) * Back_All_List[-(indx+1)][n])
            indx+=1


        #Updating Bais
        if Bais == "True":
            inds = 1
            for i in Bais_List:

                indss = 0
                for k in i:
                    k = k + ( Ln*Back_All_List[-inds][indss])
                    indss +=1

                inds+=1


sumoftrue = 0


#Training Acc
for i in range(90):
    ind = 0
    indexOfList = 0
    HiddenLayersNeuronsActivation = []
    X_Train = TrainingFeatures[ : , i:i+1]
    for j in All_List:
        NeuronLayersTemp = []
        for y in range(j.shape[1]):
            Neu = 0
            F = X_Train * j[:, y:y+1]
            for z in range(len(F)):
                Neu += F[z]
            NeuronLayersTemp.append(Neu)

        if Bais == "True":
            AllValues = np.array(NeuronLayersTemp) + np.array(Bais_List[indexOfList])
        else:
            AllValues = np.array(NeuronLayersTemp)
        Add = CalcuatingActivation(Activation, AllValues)
        HiddenLayersNeuronsActivation.append(Add)
        X_Train = HiddenLayersNeuronsActivation[ind]
        ind += 1
        indexOfList += 1
    Predicted_Output = np.array(HiddenLayersNeuronsActivation[-1])

    max = -1000
    indexOfOut = 0
    classnum = 4

    for p in range(len(Predicted_Output)):

        if Predicted_Output[p,0] < 0 and Activation == "Hyperbolic Tangent":
            Predicted_Output[p,0] = -Predicted_Output[p,0]

        if Predicted_Output[p,0] > max:
            max = Predicted_Output[p,0]
            classnum = indexOfOut
        indexOfOut+=1

    for p in range(len(Predicted_Output)):
        if p == classnum:
            Predicted_Output[p,0] = 1
        else:
            Predicted_Output[p,0] = 0

    Actual_Output = np.array(TrainingLables[:, i:i+1]).reshape(3, 1)
    summition = 0
    for a in range(3):
        if Actual_Output[a] == Predicted_Output[a]:
            summition+=1
    if summition == 3:
        sumoftrue+=1.


Accuracy = (sumoftrue/90)*100
print(Accuracy)

sumoftrue = 0
#Testing
for i in range(60):
    ind = 0
    indexOfList = 0
    HiddenLayersNeuronsActivation = []
    X_test = TestFeature[ : , i:i+1]
    for j in All_List:
        NeuronLayersTemp = []
        for y in range(j.shape[1]):
            Neu = 0
            F = X_test * j[:, y:y+1]
            for z in range(len(F)):
                Neu += F[z]
            NeuronLayersTemp.append(Neu)

        if Bais == "True":
            AllValues = np.array(NeuronLayersTemp) + np.array(Bais_List[indexOfList])
        else:
            AllValues = np.array(NeuronLayersTemp)
        Add = CalcuatingActivation(Activation, AllValues)
        HiddenLayersNeuronsActivation.append(Add)
        X_test = HiddenLayersNeuronsActivation[ind]
        ind += 1
        indexOfList += 1
    Predicted_Output = np.array(HiddenLayersNeuronsActivation[-1])

    max = -1000
    indexOfOut = 0
    classnum = 4

    for p in range(len(Predicted_Output)):

        if Predicted_Output[p,0] < 0 and Activation == "Hyperbolic Tangent":
            Predicted_Output[p,0] = -Predicted_Output[p,0]

        if Predicted_Output[p,0] > max:
            max = Predicted_Output[p,0]
            classnum = indexOfOut
        indexOfOut+=1

    for p in range(len(Predicted_Output)):
        if p == classnum:
            Predicted_Output[p,0] = 1
        else:
            Predicted_Output[p,0] = 0

    Actual_Output = np.array(TestingLables[:, i:i+1]).reshape(3, 1)
    summition = 0
    for a in range(3):
        if Actual_Output[a] == Predicted_Output[a]:
            summition+=1
    if summition == 3:
        sumoftrue+=1.


Accuracy = (sumoftrue/60)*100
print(Accuracy)









