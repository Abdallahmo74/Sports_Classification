import numpy as np
from scipy.signal import find_peaks
import scipy.integrate as integrate
from statsmodels.tsa.ar_model import AutoReg
from pywt import wavedec
import pywt


#Train

#MORPHOLOGICAL FEATURES
Data = np.load('Preprocessing_train3.npy' , allow_pickle=True)


def Feature_Extraction(signal):
    temp1 = []
    temp2 = []
    for signal_temp1 in signal[0]:
        temp1.append(signal_temp1[0])
    for signal_temp2 in signal[1]:
        temp2.append(signal_temp2[0])

    I_h = integrate.simpson(temp1)
    I_v = integrate.simpson(temp2)
    peaks_h,_ = find_peaks(temp1)
    peaks_v, _ = find_peaks(temp2)
    Y_h= []
    Y_v =[]
    for i in range(len(peaks_h)-1):
        L = peaks_h[i]
        Y_h.append(signal[0][L])
    for i in range(len(peaks_v)-1):
        L = peaks_v[i]
        Y_v.append(signal[1][L])
    peak_value_h = max(Y_h)
    peak_value_v = max(Y_v)

    feature_1_h = peak_value_h
    feature_2_h = I_h

    feature_1_v = peak_value_v
    feature_2_v = I_v

    model_h = AutoReg(temp1 , lags = 4)
    model_fit_h = model_h.fit()
    feature_3_h = model_fit_h.params

    model_v = AutoReg(temp2, lags=4)
    model_fit_v = model_v.fit()
    feature_3_v = model_fit_v.params

    coff_h = wavedec(temp1, 'db1', level=2)
    feature_4_h =  pywt.waverec([coff_h[0] , coff_h[1]] ,  'db1')



    coff_v = wavedec(temp2, 'db1', level=2)
    feature_4_v = pywt.waverec([coff_v[0],coff_v[1]], 'db1')


    signal_features = ([feature_1_h , feature_2_h , feature_3_h , feature_4_h , feature_1_v , feature_2_v , feature_3_v , feature_4_v , signal[2]])
    return signal_features

def Feature_Extraction_gui(signal):
    temp1 =[]
    temp2 = []

    for signal_temp1 in signal[0]:
        temp1.append(signal_temp1[0])
    for signal_temp2 in signal[1]:
        temp2.append(signal_temp2[0])
    I_h = integrate.simpson(temp1)
    I_v = integrate.simpson(temp2)
    peaks_h,_ = find_peaks(temp1)
    peaks_v, _ = find_peaks(temp2)
    Y_h= []
    Y_v =[]
    for i in range(len(peaks_h)-1):
        L = peaks_h[i]
        Y_h.append(signal[0][L])
    for i in range(len(peaks_v)-1):
        L = peaks_v[i]
        Y_v.append(signal[1][L])
    peak_value_h = max(Y_h)
    peak_value_v = max(Y_v)

    feature_1_h = peak_value_h
    feature_2_h = I_h

    feature_1_v = peak_value_v
    feature_2_v = I_v

    model_h = AutoReg(temp1 , lags = 4)
    model_fit_h = model_h.fit()
    feature_3_h = model_fit_h.params

    model_v = AutoReg(temp2, lags=4)
    model_fit_v = model_v.fit()
    feature_3_v = model_fit_v.params

    coff_h = wavedec(temp1, 'db1', level=2)
    feature_4_h =  pywt.waverec([coff_h[0] , coff_h[1]] ,  'db1')



    coff_v = wavedec(temp2, 'db1', level=2)
    feature_4_v = pywt.waverec([coff_v[0],coff_v[1]], 'db1')

    signal_features = ([feature_1_h , feature_2_h , feature_3_h , feature_4_h , feature_1_v , feature_2_v , feature_3_v , feature_4_v])
    return signal_features


#Train
# All_signal_features_train = []
# for signal in Data:
#     signal_features = Feature_Extraction(signal)
#     All_signal_features_train.append(signal_features)
# np.save('Train_All_features3.npy' , All_signal_features_train)


#Test
# All_signal_features_test = []
# Data = np.load('Preprocessing_test3.npy' , allow_pickle=True)
# for signal in Data:
#     signal_features = Feature_Extraction(signal)
#     All_signal_features_test.append(signal_features)
# np.save('Test_All_features3.npy' , All_signal_features_test)




