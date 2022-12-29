import random
import numpy as np

# произвольное двоичное слово длиной n
def rand_bin(n):
    bin_str=[]
    for i in range(n):
       temp = random.randint(0, 1)
       bin_str.append(temp)
    return bin_str

def mod2(x: int):
    return x % 2

# вычислять проверочные биты по группам 
def check_bits(matrix, groups, k1, k2):
    xr=[]
    for i in range (0,2):
        xr_temp = np.sum(matrix, axis=i)
        xr = np.append(xr, mod2(xr_temp)).astype(int)
    if (groups >= 3):
        for i in range (int(-k1/2 - 1), k2): 
            xr_temp = np.sum(np.diag(matrix, i))
            xr = np.append(xr, mod2(xr_temp))
    if (groups == 4):
        for i in range (int(-k1/2 - 1), k2):
            xr_temp = np.sum(np.diag(np.fliplr(matrix), i))
            xr = np.append(xr, mod2(xr_temp))
    return xr

# генерировать ошибку произвольной кратности
def rand_mistake(matrix, k1, k2, amount = 1):
    yk = matrix
    for n in range (amount):
        i = random.randint(0, k1-1)
        j = random.randint(0, k2-1)
        if (yk[i,j] == 0):
            yk[i,j] = 1
        else:
            yk[i,j] = 0
    return yk

# определять местоположение ошибочных символов и исправлять ошибочные символы
def find_mistake(yr, xr, k1, k2, yk):
    n = 0
    i = []
    j = []
    corr = yk
    while n < k2:
        if (yr[n] != xr[n]):
            j.append(n)
        n += 1
    while n < (k1 + k2):
        if (yr[n] != xr[n]):
            i.append(n-k2)
        n += 1
    # print("Ошибочные символы по i:",i)
    # print("Ошибочные символы по j:",j)
    for elem_i in i:
        for elem_j in j:
            if (corr[elem_i,elem_j] == 0):
                corr[elem_i,elem_j] = 1
            else:
                corr[elem_i,elem_j] = 0
    return corr
    


