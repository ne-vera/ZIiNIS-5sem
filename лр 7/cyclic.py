import numpy as np
import random

# Подсчитывает число информационных разрядов
def count_k(sequence):
    if type(sequence) == int:
        sequence = bin(sequence)[2:]
    return len(sequence)

# Возвращает степень образующего полинома
def count_power(polynomial):
    return count_k(polynomial) - 1

# Делит делимое на делитель, вместо вычитания - XOR
def divide_with_xor(dividend, divisor):
    dividend_digit_count = count_k(dividend)
    divisor_digit_count = count_k(divisor)
    if dividend_digit_count < divisor_digit_count:
        return 0, dividend
    i = dividend_digit_count - divisor_digit_count + 1  # итерации
    quotient = 0  # частное
    remainder = 0  # остаток
    part = dividend >> i - 1
    while i > 0:
        i -= 1
        quotient <<= 1
        if count_k(part) >= divisor_digit_count:
            quotient += 1
            part ^= divisor
        else:
            quotient += 0
        if i == 0:
            remainder = part
        else:
            next_digit = (dividend >> (i - 1)) & 0b1
            part <<= 1
            part += next_digit
    return quotient, remainder

#  Кодирует принципами циклического кода инф. последовательность
def encrypt_with_cyclic_code(sequence, g, k, n):
    # 1. умножаем полином инф. посл-ти на x^r
    # фактически выполняем сдвиг влево
    shifted = sequence + '0' * (n - k)

     # 2. вычисляем остаток от деления получившегося полинома на g(x)
    mod = divide_with_xor(int(shifted, 2), int(g, 2))[1]

    # 3. конкатенация
    mod_len = len(bin(mod)[2:])
    return sequence + '0' * (n - k - mod_len) + bin(mod)[2:]

# Составляет порождающую матрицу по полиному
def generate_polynomial(g, n, k):
    g_x = [int(x) for x in g]
    g_x = [*g_x, * np.zeros(n-len(g_x), dtype=int)]
    generating_matrix = g_x
    for j in range (k):
        buf = np.roll(g_x, j, axis=0)
        if j > 0 :
            generating_matrix = np.vstack((generating_matrix, buf))
    return generating_matrix

# Приводит порождающую матрицу к каноническому виду
def generate_canonical(k, n, g):
    G = np.zeros([k, n], dtype=int)
    g_x = [int(x) for x in g]
    for i in range(0, k):
        G[i][i] = 1
    for i in range(0, k):
        q, r = np.polydiv(G[i], g_x)
        for k in range(0, len(r)):
            buff = (r[k] % 2)
            r[k] = buff
        new_result = np.polyadd(G[i], r)
        G[i] = new_result
    return G

# Составляет проверочную матрицу канонического вида
def generate_check(canonical_matrix, k, n):
    check_matrix = []
    for i in range(k, n):
        check_matrix.append(canonical_matrix[:, i].transpose())
    i_matrix = np.eye(k-1, dtype=int)  
    return np.hstack((check_matrix, i_matrix))

# Позиция ошибки определяется (генерируется) случайным образом. 
def rand_mistake(encoded, num):
    y = [int(x) for x in encoded]
    for n in range (num):
        i = random.randint(0, len(encoded)-1)
        if (y[i] == 0):
            y[i] = 1
        else:
            y[i] = 0
    y = ''.join(map(str, y))
    return y

def count_syndrome(y_n, g):
    return bin(divide_with_xor(int(y_n, 2), int(g, 2))[1])[2:].rjust(5, '0')

def error_position(syndrome, check_matrix, n):
    error_vector = np.zeros(n, dtype=int)
    for i in range(n):
        if (''.join(map(str, check_matrix[:, i].transpose())) == syndrome):
            error_vector[i] = 1
    error_vector = ''.join(map(str, error_vector))
    return error_vector

def xor(a: str, b: str) -> str:
    delta_length = abs(len(a) - len(b))
    if len(a) > len(b):
        for i in range(delta_length):
            b = '0' + b
    elif len(b) > len(a):
        for i in range(delta_length):
            a = '0' + a
    result = ""
    for i in range(len(a)):
        if a[i] == b[i]:
            result += '0'
        else:
            result += '1'
    return result
