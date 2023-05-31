import statistics
from scipy import signal
from scipy.signal import butter , filtfilt
import numpy as np
from sklearn.preprocessing import StandardScaler

#FILTERING & RESAMPLING & REMOVING DC COMPONENT



train_signal = np.load('Train_data.npy', allow_pickle=True)
test_signal = np.load('Test_data.npy', allow_pickle=True)

filtered_train_signal = []
filtered_test_signal = []

def Signal_preprocessing(Signal):
    def butter_bandpass_filter(Input_signal, Low_cutoff, High_cutoff, sampling_rate, order):
        nyq = 0.5 * sampling_rate
        low = Low_cutoff / nyq
        high = High_cutoff / nyq
        Numenator, denominator = butter(order, [low, high], btype='band', output='ba', analog=False, fs=None)
        filtered = filtfilt(Numenator, denominator, Input_signal)
        return filtered
    Scalar = StandardScaler()
    #Signal = Scalar.fit_transform(np.array(Signal).reshape(-1,1))
    full_signal = []
    signal1 = []
    for i in Signal:
        signal1.append(i)
    filtered_h = butter_bandpass_filter(signal1[0:250], Low_cutoff=0.5, High_cutoff=20, sampling_rate=176, order=2)
    resample_h = signal.resample(filtered_h, 50)
    mean_h = statistics.mean(resample_h)
    Removed_DC_h = [resample_h[i] - mean_h for i in range(len(resample_h))]
    Removed_DC_h = Scalar.fit_transform(np.array(Removed_DC_h).reshape(-1,1))
    filtered_v = butter_bandpass_filter(signal1[250:500], Low_cutoff=0.5, High_cutoff=20, sampling_rate=176, order=2)
    resample_v = signal.resample(filtered_v, 50)
    mean_v = statistics.mean(resample_v)
    Removed_DC_v = [resample_v[i] - mean_v for i in range(len(resample_v))]
    Removed_DC_v = Scalar.fit_transform(np.array(Removed_DC_v).reshape(-1,1))
    return Removed_DC_h , Removed_DC_v



def Signal_preprocessing_gui(Signal):
    def butter_bandpass_filter(Input_signal, Low_cutoff, High_cutoff, sampling_rate, order):
        nyq = 0.5 * sampling_rate
        low = Low_cutoff / nyq
        high = High_cutoff / nyq
        Numenator, denominator = butter(order, [low, high], btype='band', output='ba', analog=False, fs=None)
        filtered = filtfilt(Numenator, denominator, Input_signal)
        return filtered
    Scalar = StandardScaler()
    #Signal = Scalar.fit_transform(np.array(Signal).reshape(-1,1))
    temp_signal = []
    for i in Signal:
        temp_signal.append(i)
    filtered_h = butter_bandpass_filter(temp_signal[0:250], Low_cutoff=0.5, High_cutoff=20, sampling_rate=176, order=2)
    resample_h = signal.resample(filtered_h, 50)
    mean_h = statistics.mean(resample_h)
    Removed_DC_h = [resample_h[i] - mean_h for i in range(len(resample_h))]
    Removed_DC_h = Scalar.fit_transform(np.array(Removed_DC_h).reshape(-1,1))
    filtered_v = butter_bandpass_filter(temp_signal[250:500], Low_cutoff=0.5, High_cutoff=20, sampling_rate=176, order=2)
    resample_v = signal.resample(filtered_v, 50)
    mean_v = statistics.mean(resample_v)
    Removed_DC_v = [resample_v[i] - mean_v for i in range(len(resample_v))]
    Removed_DC_v = Scalar.fit_transform(np.array(Removed_DC_v).reshape(-1, 1))

    return Removed_DC_h , Removed_DC_v




# for signal_Temp in train_signal:
#     R_DC_h , R_DC_v = Signal_preprocessing(signal_Temp[0])
#     filtered_train_signal.append([R_DC_h , R_DC_v , signal_Temp[1]])
#
#
# for signal_temp in test_signal:
#     R_DC_h , R_DC_v = Signal_preprocessing(signal_temp[0])
#     filtered_test_signal.append([R_DC_h , R_DC_v, signal_temp[1]])


# np.save('Preprocessing_train3.npy' , filtered_train_signal)
# np.save('Preprocessing_test3.npy' , filtered_test_signal)









