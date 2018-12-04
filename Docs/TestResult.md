|Cценарий|Действие|Ожидаемый результат|Фактический результат| Оценка|
|:---|:---|:---|:---|:---|
|001-1: Вход без Интернет подключения | 1. Проверьте отсутствие подключения к интернету. 2. Открыть приложение. | Приложение работает | Приложение работает| Тест пройден|
|001-2: Вход с Интернет подключением | 1. Открыть приложение.| Приложение работает |Приложение работает | Тест пройден|
|002-1: Переход из меню в игру | 1. Открыть приложение. 2. Нажать кнопку "Play"| Игра начинается | Игра начинается|Тест пройден |
|002-2: Переход с экрана проигрыша в меню. | 1. Находясь в игре проиграть. 2. Нажать на кнопку "Ok" | Откроется главное меню | Открылось главное меню | Тест пройден|
|002-3: Переход с экрана меню в магазин и обратно | 1. Наъодясь в меню, перейти в магазин. 2. Находясь в магазине нажать на кнопку "Menu" | Переход в магазин, потом с магазина в меню | Произошел переход в магазин, и выход в меню| Тест пройден|
|003-1: Корректное отображение максимального счета в поп-апе проигрыша | 1.Находясь в игре, побить рекорд и запомнить счет. | Показ максимального счета | Счет показывается|  Тест пройден|
|003-2: Сохранение максимального количества очков при выхое из приложения в различных игровых состояний - из меню, самой игры, экрана проигрыша и магазина. | 1.Находясь в любой состоянии выйти из игры. 2. Зайти в игру. 3. Проиграть и посмотреть максимальный счет | Правильный показ максимального счета | Максимальное количество не сохраняется при ывходе из приложения во время игры| Тест не пройден|
|004-1: Корректное отображение счета после проигрыша | 1.Находясь в игре, запомнить количество очков и проиграть | Корректное отображение очков | Очки отображаются корректно | Тест пройден|
|005-1: Покупка коробки с количеством звездочек, меньшим чем стоимость коробки | 1. Имеея количества звездочек меньше, чем стоимость коробки зайти в магазин. 2. Купить коробку | Коробка не покупается | Неудача при покупке| Тест пройден|
|005-2: Покупка коробки при достаточном количестве звездочек | 1. Имеея количества звездочек больше, чем стоимость коробки зайти в магазин. 2. Купить коробку | Коробка купилась | Покупка удалась| Тест пройден |
|005-3: Сохранение купленной коробки после выхода из игры и последующего входа в неё | 1.Купить коробку. 2. Выйти из игры. 3. Войти в игру | Коробка останется купленной | Сохранение коробки не выпоняется при выходе из игры на экране магазина| Тест не пройден|
|006-1: Правильное соприкосновение коробки с звездочками | 1. Находясь в игре соприкоснутся коробков со звездочкой| Удаление звездочки и увеличение счетчика звездочек на 1 | Звезда удалилась и количество звезд увеличилось на 1 | Тест пройден|
|006-2: Правильное сохранение количества звездочек после покупки коробки | 1. Запомнить количество звездочек. 2. Купить коробку. 3. Проверить количество звездочек | Уменьшение звездочек на кстоимость коробки. | Количество звезд уорректно уменьшилось|Тест пройден|
|007-1: При нажатии по экрану коробка падает вниз. | 1. Войти в игру. 2. Нажать по экрану | Коробка падает вниз | Коробка упала| ТЕст пройден|
|008-1: Производительность | 1. Взять слабый девайс. 2. Запустить игру. 3. Играть некоторое время | Игра должна быть плавной | Лагов не было, фризов тоже | Тест пройден|
|008-2: Понятный гемплей - понимание того, что надо делать с 1-3 игры | 1. Запустить игру. 2. Сыграть раза 3. | Гемплей должен быть понятен | Изначально непонятно куда лететь дальше пока камера движется медленно | Тест не пройден|

## Замечания
* На окне главного меню нет максимального счета и количества звезд. Непонятно, где они находятся.
* Нет паузы и нельзя ее приостановить
* Pop-up проигрыша слишком маленький и прозрачный.
* Не отображается количество звездочек во время игры.
* После соприкосновении с коробкой сбоку платформы, начинаются подергивания.