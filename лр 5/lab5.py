#8 вариант 
from iterative import *
import numpy as np

# Длина информационного слова k = 24 
k = 24

print('------------------------ Двумерная матрица ------------------------\n')

# произовльное двоичное представление информционного слова
xk = rand_bin(k)
print("Информационное слово: ", " ".join(map(str, xk)))

# вписать произвольное Xk в двумерную матрицу
k1 = 4
k2 = 6

# k1 = 3
# k2 = 8

matrix = np.reshape(xk, (k1, k2))
print("Матрица k1*k2:\n", matrix)

groups = 3
print("Количество групп паритетов =", groups)

xr = check_bits(matrix, groups, k1, k2)
print("Проверочные биты:", " ".join(map(str, xr)))

# формировать кодовое слово Xn
xn = np.hstack([xk, xr])
print("Кодовое слово:", " ".join(map(str, xn)))

mistake_amount = 2
yk = rand_mistake(matrix, k1, k2, mistake_amount)
print("Матрица принятого сообщения:\n", yk)

# опеределить местоположение ошибочных символов итеративным кодом и исправить
yr = check_bits(matrix, groups, k1, k2)
print("Проверочные биты принятого слова:"," ".join(map(str, yr)))

if (xr == yr).all():
	print ("Xr и Yr одинаковые, ошибок не обнаружено")
else:
    print ("Xr и Yr не одинаковые")
    #определять местоположение ошибочных символов итеративным кодом в слове Yn
    # #исправлять ошибочные символы
    corr = find_mistake(yr, xr, k1, k2, yk)
    print("Исправленные ошибочные символы: \n", corr)

print('---------------------- Корректирующая способность кода ----------------------\n')
words_number = 50
# Корректирующая способность для 1 ошибки
corr_number = 0
mistake_amount = 1
for i in range(words_number):
    yk = rand_mistake(matrix, k1, k2, mistake_amount)
    yr = check_bits(matrix, groups, k1, k2)
    corr = find_mistake(yr, xr, k1, k2, yk)
    if (xk == np.reshape(corr, (k1*k2, ))).all():
        corr_number += 1
print(f'Корректирующая способность для 1 ошибки: {corr_number / words_number}')
# Корректирующая способность для 2 ошибок
mistake_amount = 1
for i in range(words_number):
    yk = rand_mistake(matrix, k1, k2, mistake_amount)
    yr = check_bits(matrix, groups, k1, k2)
    corr = find_mistake(yr, xr, k1, k2, yk)
    if (xk == np.reshape(corr, (k1*k2, ))).all():
        corr_number += 1
print(f'Корректирующая способность для 2 ошибок: {corr_number / words_number}')

print('\n----------------------- Трёхмерная матрица -----------------------\n')

# вписать произвольное Xk в трехмерную матрицу
k1 = 6
k2 = 2
z = 2

matrix = np.reshape(xk, (z, k1, k2))
print("Матрица k1*k2*z:\n", matrix)

groups = 3
print("Количество групп паритетов =", groups)

print("Матрица k1*k2:\n", matrix[0])

xr = check_bits(matrix[0], groups, k1, k2)
print("Проверочные биты:", " ".join(map(str, xr)))

xn = np.hstack([xk, xr])
print("Кодовое слово:", " ".join(map(str, xn)))

mistake_amount = 2
yk = rand_mistake(matrix[0], k1, k2, mistake_amount)
print("Матрица принятого сообщения:\n", yk)

yr = check_bits(matrix[0], groups, k1, k2)
print("Проверочные биты принятого слова:"," ".join(map(str, yr)))

if (xr == yr).all():
	print ("Xr и Yr одинаковые, ошибок не обнаружено")
else:
    print ("Xr и Yr не одинаковые")

#определять местоположение ошибочных символов итеративным кодом в слове Yn
#исправлять ошибочные символы
corr = find_mistake(yr, xr, k1, k2, yk)
print("Исправленные ошибочные символы: \n", corr)