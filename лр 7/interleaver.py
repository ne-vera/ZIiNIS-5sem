import numpy as np
import random
from cyclic import *
# перевод в двоичный
def text_to_bits(text, encoding='utf-8', errors='surrogatepass'):
   bits = bin(int.from_bytes(text.encode(encoding, errors), 'big'))[2:]
   return bits.zfill(8 * ((len(bits) + 7) // 8))

# и обратно
def text_from_bits(bits, encoding='utf-8', errors='surrogatepass'):
   n = int(bits, 2)
   return n.to_bytes((n.bit_length() + 7) // 8, 'big').decode(encoding, errors) or '\0'

# делит сообщение на группы по n
def grouper(iterable, n):
   args = [iter(iterable)] * n
   return zip(*args)

# формирует матрицу информационных слов заданной длины
def div_inf_words(seq, length):
   # Добавляем нули слева
   if len(seq) % length != 0:
      for _ in range(length - len(seq) % length):
         seq = '0' + seq
   inf_words = [''.join(i) for i in grouper(seq, length)]
   return inf_words

# формирует матрицу перемежения
def generate_interleave_matrix(seq: str, col_num: int):
   # Добавляем нули слева
   if len(seq) % col_num != 0:
      for _ in range(col_num- len(seq) % col_num):
         seq = '0' + seq
   matrix = list(seq)
   row_num = int(len(seq)/col_num)
   return np.reshape(matrix, (row_num, col_num))

# перемежение
def interleave(matrix):
   return ''.join(map(str, matrix.transpose().ravel()))

# формирует случаный пакет ошибок заданной длины
def rand_mistakes(interleaved, num):
   y = [int(x) for x in interleaved]
   place = random.randint(0, len(interleaved) - num - 1)
   for i in range(place, place + num):
      if (y[i] == 0):
         y[i] = 1
      else:
         y[i] = 0
   y = ''.join(map(str, y))
   return y

# деперемежение
def deinterleave(y: str, col_num: int) -> str:
    matrix = []
    for _ in range(int(len(y) / col_num)):
        matrix.append('')
    # Формируем матрицу по столбцам
    for _ in range(col_num):
        for i in range(len(matrix)):
            matrix[i] += y[0]
            y = y[1:]
    return ''.join(matrix)

# исправление ошибок
def correct_mistakes(seq, g, check_matrix, n):
   inf_words = [''.join(i) for i in grouper(seq, n)]
   corrected_words = []
   for word in inf_words:
      syndrome = count_syndrome(word, g)
      error_vector = error_position(syndrome, check_matrix, n)
      corr_word = xor(word, error_vector)
      corrected_words.append(corr_word)
   return ''.join(corrected_words)