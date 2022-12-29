import numpy as np
import time

def get_w1(m):
    lst = list(m)
    steps = len(m)
    w1 = []
    for i in range (steps):
        buf = np.roll(lst, -i, axis=0)
        w1.append(buf)
    return w1

def get_w2(w1):
    lst = []
    for row in w1:
        buf = ''.join(row)
        lst.append(buf)
    lst = sorted(lst)
    w2 = []
    for elem in lst:
        w2.append(list(elem))
    np.reshape(w2, (len(w1), len(w1)))
    return w2

def get_bwt(m):
    w1 = get_w1(m)
    w2 = get_w2(w1)
    w2 = np.array(w2)
    k = int(len(w2) - 1)
    m_k = ''.join(w2[:, k])
    z = 0
    for i, row in enumerate(w2):
        if (''.join(row) == m):
            z = i + 1
    return m_k, z

def reverse_bwt(m_k, z):
    w = []
    for _ in range(len(m_k)):
        w.append('')
    for _ in range(len(m_k)):
        for i in range(len(m_k)):
            w[i] = m_k[i] + w[i]
        w.sort()
    return w[z-1]

def task(m):
    print('Исходное сообщение:', m)
    print('W1:')
    for row in get_w1(m):
        print(row)
    print('W2:')
    for row in get_w2(get_w1(m)):
        print(row)
    start_time = time.time()
    m_k, z = get_bwt(m)
    print(m_k, z)
    print('Время выполнения прямого преобразования:', (time.time() - start_time))
    start_time = time.time()
    res =  reverse_bwt(m_k, z)
    print('Полученное сообщение:', res)
    print('Время выполнения обратного преобразования:', (time.time() - start_time))

def text_to_bits(text, encoding='utf-8', errors='surrogatepass'):
   bits = bin(int.from_bytes(text.encode(encoding, errors), 'big'))[2:]
   return bits.zfill(8 * ((len(bits) + 7) // 8))