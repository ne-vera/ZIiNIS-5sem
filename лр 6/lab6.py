import numpy as np
from cyclic import *

# 8 вариант 
# количество избыточных символов r = 5
# полином х^5 + х^4 + х^3 + х^2 + 1
g = '111101'
print('Образующий полином =', g)
r = count_power(g)
print('r =', r)

seq = '111111'
print('Сообщение:', seq)
k = count_k(seq)
print('k =', k)

n = r + k

#2. Составить порождающую матрицу (n, k)-кода в соответствии с формулой (6.7), 
print("\nПорождающая матрица:")
for row in generate_polynomial(g, n, k):
    print(row)
# трансформировать ее в каноническую форму
canonical_matrix = generate_canonical(k, n, g)
print("\nПорождающая матрица в каноническом виде:")
for row in canonical_matrix:
    print(row)

#  и далее – в проверочную матрицу канонической формы. 
print("\nПроверочная матрица:")
check_matrix = generate_check(canonical_matrix, k, n)
for row in check_matrix:
    print(row)

#3. Используя порождающую матрицу ЦК, вычислить избыточные символы (слово Xr) кодового слова Xn 
# и сформировать это кодовое слово
encoded = encrypt_with_cyclic_code(seq, g, k, n)
print('Кодовое слово:', encoded)

# 4. Принять кодовое слово Yn со следующим числом ошибок
# 5. Для полученного слова Yn вычислить и проанализировать синдром. 
# В случае, если анализ синдрома показал, что информационное сообщение было передано с ошибкой (или 2 ошибками), сгенерировать унарный вектор ошибки Еn = е1, е2, …, еn 
# и исправить одиночную ошибку, используя выражение (6.5);
# проанализировать ситуацию при возникновении ошибки в 2 битах. 
# 0; 
y_n = rand_mistake(encoded, 0)
print('\nПриянтое сообщение без ошибок:', y_n)
syndrome = count_syndrome(y_n, g)
print ("Синдром:", syndrome)
# 1; 
y_n = rand_mistake(encoded, 1)
print('\nПриянтое сообщение, 1 ошбика:', y_n)
syndrome = count_syndrome(y_n, g)
print ("Синдром:", syndrome)
error_vector = error_position(syndrome, check_matrix, n)
print('Вектор ошибки:', error_vector)
print('Исправленное сообщение:', xor(y_n, error_vector))
# 2. 
y_n = rand_mistake(encoded, 2)
print('\nПриянтое сообщение, 2 ошибки:', y_n)
syndrome = count_syndrome(y_n, g)
print ("Синдром:", syndrome)
error_vector = error_position(syndrome, check_matrix, n)
print('Вектор ошибки:', error_vector)
print('Исправленное сообщение:', xor(y_n, error_vector))
