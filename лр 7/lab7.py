from interleaver import *
from cyclic import *
import numpy as np
# используется блочный перемежитель/деперемежитель. 
# 8 вариант:
# циклический, длина пакета ошибок: 3,6,7 
# число столбцов в матрице 7 
# длинна сообщения, байт 13 
# длина информационного слова к бит 7

k = 7
col_num = 7

seq = 'helloworldab'
print('Исходное сообщение длиной 13:', seq)

bin_seq = text_to_bits(seq)
print('Исходное сообщение в бинарном виде:', bin_seq)

print('Информационные слова длиной k = 7:')
inf_words = div_inf_words(bin_seq, k)
for row in inf_words:
    print(row)

g = '1000011'
r = count_power(g)
n = r + k
canonical_matrix = generate_canonical(k, n, g)
check_matrix = generate_check(canonical_matrix, k, n)

encoded = ''
for row in inf_words:
    encoded += encrypt_with_cyclic_code(row, g, k, n)

number_of_added_zeros = 0
if len(encoded) % col_num != 0:
    for _ in range(col_num - len(encoded) % col_num):
        encoded = '0' + encoded
        number_of_added_zeros += 1

print('Закодированное сообщение:', encoded)

print('Матрица перемежения:')
interleave_matrix = generate_interleave_matrix(encoded, col_num)
for row in interleave_matrix:
    print(row)

interleaved = interleave(interleave_matrix)
print(interleaved)

mistaken = rand_mistakes(interleaved, 3)
print(mistaken)

deinterleaved = deinterleave(mistaken, col_num)
print('Деперемеженное сообщение:', deinterleaved)

corrected = correct_mistakes(deinterleaved[number_of_added_zeros:], g, check_matrix, n)
print('Сообщение с исправленными ошибками:', corrected)

print('Ошибки исправлены:', corrected == encoded)

print("Корректирующие способности:")
# Для 3 ошибок
total = 100
right = 0
for _ in range(total):
    mistaken = rand_mistakes(interleaved, 3)
    deinterleaved = deinterleave(mistaken, col_num)
    corrected = correct_mistakes(deinterleaved[number_of_added_zeros:], g, check_matrix, n)
    if (corrected == encoded):
        right += 1
print(f"Для 3 ошибок: {right / total}")

right = 0
for _ in range(total):
    mistaken = rand_mistakes(interleaved, 6)
    deinterleaved = deinterleave(mistaken, col_num)
    corrected = correct_mistakes(deinterleaved[number_of_added_zeros:], g, check_matrix, n)
    if (corrected == encoded):
        right += 1
print(f"Для 6 ошибок: {right / total}")

right = 0
for _ in range(total):
    mistaken = rand_mistakes(interleaved, 7)
    deinterleaved = deinterleave(mistaken, col_num)
    corrected = correct_mistakes(deinterleaved[number_of_added_zeros:], g, check_matrix, n)
    if (corrected == encoded):
        right += 1
print(f"Для 7 ошибок: {right / total}")