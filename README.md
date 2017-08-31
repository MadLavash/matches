# Matches
Пример небольшой игры. Код был полностью написан мной.

Игра создавалась по нижеследующему ТЗ и полностью удовлетворяет его условия.

### Правила игры.
На стол выкладывается случайное число спичек (от 20 до 45). Человек и компьютер ходят по-
очереди, на каждом ходу убирая со стола определённое число спичек. Первым ходит человек.
Первым ходом можно убрать от одной до трёх спичек. На каждом последующем ходе можно
убрать от n-1 до n+1 спичек, но не менее 1 (n – количество спичек, убранных противником на
предыдущем ходу). Выигрывает игрок, убравший последние спички.

### Требования к программе.
Игра должна быть написана на Unity3d. Необходимо отображать оставшиеся спички и количество
спичек, взятых противником. Не стоит акцентироваться на визуальных эффектах и красивостях, но
тем не менее интерфейс должен быть визуальным, управление осуществляться с помощью
мыши. По окончанию игры вывести надпись: «Вы победили!» или «Вы проиграли!» в зависимости
от результатов игры.

### Важно (выполнение следующего пункта даст вам огромный плюс при рассмотрении вашей кандидатуры):
Разработать логику для AI таким образом, чтобы всегда выбирался оптимальный ход из
возможных. Т.е. компьютер должен выигрывать всегда, если начальное количество спичек это
позволяет (даже при оптимальной игре человека). Ограничение времени на ход компьютера: 2
секунды (на intel i7 3.5GHz).
Необходимые для решения темы: программирование игр двух лиц с полной информацией,
элементы динамического программирования
