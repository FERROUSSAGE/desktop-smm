   M S B A M L   `   `   `  ����F  Bdesktop-smm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:desktop_smm.Pagesdesktop_smm.Pages  5           desktop_smm.Pages.InfoPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:desktop_smm.Pages     5        N� Title$  InfoPage��5	       N� 
Background
 �����.��5      � $��0 90 0 0q�6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��230 �6   .+�5   
   �� $��
50 0 40 0 q�5      .+�5      �� -   5         ��Name$ tbTitle��5         BaseText#��[  5      $��24(�5        ColorTextAccent#��[ 5      $��0 0 0 45q�5      �� 5      -   5        $ tbDescription��5      #��[  5      $��18(�5       �� 
LineHeight$ 28��5      $
��Wrap=�5      5      �� 5!   
   -   5         ��Name$ 	stButtons��5"      $��1��5#      $��0 25 0 0q�5$      .+�5%      ��   
ButtonInfo#��[ 5&      .��5'      �� $%��Как это работает?��5(      
 � Tag$� �Все просто!
Приложение предназначено для упрощения работы операторам SMM-CRAFT.ru, здесь собраны несколько ресселеров, при помощи которых оформляются заказы. Также, разработан модуль телеграма, чтобы непосредственно напрямую в приложение писали клиенты с интересующими вопросами. Ко всему прочем, реализована отправка писем, браузер и список оформленных заказов, который можно выгрузить в Google DOCS��5)        InfoButtonText#��[ 5*      5+      �� 5,      #��[ 5-      $��	0 10 0 10q�5.      .��5/      �� $6��0Правила оформления заказа��50      $� �Чтобы создать компанию, нужно:
1. Выбрать ресселер
2. Выбрать тип(Лайки, просмотры и т.д)
3. Заполнить поля ввода верно, обязательно править ссылку, а также подгонять под соц. сеть
4. Выбрать тип платежной системы(Из админки), а также заполнить сумму заказа
5. Создайте компанию, кликнув по соотвествующей кнопке��51      #��[ 52      53      �� 54      #��[ 55      $��	0 10 0 10q�56      .��57      �� $D��>Как пользоваться Списком заказов?��58      $� �На странице Список заказов, отображены все созданные компании
К каждому заказу можно написать комментарий для личных целей, также все заказы сортированы по дате их создания.
Если нужен конкретный заказ, то воспользуйтесь поиском
У каждого заказа можно проверить статус его выполнения
Если Вам нужно добавить заказ, воспользуйтесь соответствующей кнопкой
Для выгрузки заказов за текущие число, воспользуйтесь соотвествующей кнопкой. После Вам нужно будет подкорректировать таблицу уже на сайте Google DOCS��59      #��[ 5:      5;      5<      5=      5>      