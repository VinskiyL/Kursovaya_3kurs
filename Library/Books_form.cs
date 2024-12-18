﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Library
{
    public partial class Books_form : Form
    {
        public Books_form(int id)
        {
            this.id = id;
            InitializeComponent();
        }
        public Books_form()
        {
            InitializeComponent();
        }

        Authors_books a_b = new Authors_books();
        Books books = new Books();
        int id = 0;
        int select = 0;


        private void Books_form_Load(object sender, EventArgs e)
        {
            try
            {
                books.Add();
                Authors.Enabled = true;
                textBox2.Text = "Список книг";
                if (books == null)
                {
                    throw new Exception("Нет информации о книгах");
                }
                if (id == 0)
                {
                    for (int i = 0; i < books.books.Count; i++)
                    {
                        dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                            books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                    }
                }
                else
                {
                    a_b.Add();
                    textBox2.Text = "Список книг автора";
                    textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                    Authors.Enabled = false;
                    for (int i = 0; i < books.books.Count; i++)
                    {
                        if (a_b.Find(id, books.books[i].index_))
                        {
                            dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                             books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получаем выбранную строку
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                select = (int)selectedRow.Cells["index"].Value;
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Book_form form = new Book_form(select, books);
                form.DataUpdated += DataUpdated;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataUpdated()
        {
            dataGridView1.Rows.Clear();
            if (id == 0)
            {
                for (int i = 0; i < books.books.Count; i++)
                {
                    dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                        books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                }
            }
            else
            {
                for (int i = 0; i < books.books.Count; i++)
                {
                    if (a_b.Find(id, books.books[i].index_))
                    {
                        dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                         books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                    }
                }
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DialogResult result = MessageBox.Show("Вы хотите добавить уже существующую книгу?", "Выбор действия",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    textBox2.Text = "Список книг, не принадлежащих данному автору";
                    textBox1.Text = "Напишите индексы книг в текстовом поле внизу окна";
                    Update.Enabled = false;
                    Insert.Enabled = false;
                    Delete.Enabled = false;
                    Authors.Enabled = false;
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < books.books.Count; i++)
                    {
                        if (!a_b.Find(id, books.books[i].index_))
                        {
                            dataGridView1.Rows.Add(books.books[i].index_, books.books[i].mark, books.books[i].title_, books.books[i].place,
                                books.books[i].info, books.books[i].date, books.books[i].volume_, books.books[i].total, books.books[i].now);
                        }
                    }
                    System.Windows.Forms.TextBox readOnlyTextBox = new System.Windows.Forms.TextBox();
                    readOnlyTextBox.ReadOnly = true;
                    readOnlyTextBox.BorderStyle = BorderStyle.None;
                    readOnlyTextBox.Font = new Font(readOnlyTextBox.Font.FontFamily, 12);
                    readOnlyTextBox.Text = "Напишите номера книг через запятую и нажмите Enter";
                    readOnlyTextBox.Size = new Size(535, 30); // Установите размер
                    readOnlyTextBox.Location = new Point(704, 481);

                    // Создание второго текстового бокса для ввода
                    System.Windows.Forms.TextBox inputTextBox = new System.Windows.Forms.TextBox();
                    inputTextBox.Font = new Font(inputTextBox.Font.FontFamily, 12);
                    inputTextBox.Size = new Size(535, 30); // Установите размер
                    inputTextBox.Location = new Point(704, 518);
                    inputTextBox.ScrollBars = ScrollBars.Horizontal;

                    inputTextBox.KeyPress += (sender0, e0) =>
                    {
                        if (!char.IsControl(e0.KeyChar) && !char.IsDigit(e0.KeyChar) && e0.KeyChar != ',')
                        {
                            // Если нет, отменяем событие
                            e0.Handled = true;
                        }
                    };

                    // Обработчик события KeyDown для второго текстового бокса
                    inputTextBox.KeyDown += (sender1, e1) =>
                    {
                        if (e1.KeyCode == Keys.Enter)
                        {
                            // Логика для обработки введенных данных
                            string inputText = inputTextBox.Text;

                            // Здесь вы можете добавить код для обработки введенных номеров книг
                            string[] bookNumbers = inputText.Split(',');

                            // Пример: поиск и добавление книг по введенным номерам
                            foreach (var number in bookNumbers)
                            {
                                if (int.TryParse(number.Trim(), out int bookNumber))
                                {
                                    if (books.Find(bookNumber) != null)
                                    {
                                        if (!a_b.Find(id, bookNumber))
                                        {
                                            int i = a_b.FindMaxId();
                                            Author_book a = new Author_book(i + 1, id, bookNumber);
                                            a_b.AddDb(a);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Книга с индексом " + bookNumber + " была ранее добавлена данному автору");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Книги с индексом " + bookNumber + " не существует");
                                    }
                                }
                            }

                            // Выход из метода или закрытие формы
                            DataUpdated();
                            textBox2.Text = "Список книг автора";
                            textBox1.Text = "Для редактирования записи в таблице кликните на любой элемент строки, а затем по нужной кнопке";
                            Update.Enabled = true;
                            Insert.Enabled = true;
                            Delete.Enabled = true;
                            Authors.Enabled = true;
                            this.Controls.Remove(inputTextBox);
                            this.Controls.Remove(readOnlyTextBox);
                            return;
                        }
                    };

                    // Добавление текстовых боксов на форму
                    this.Controls.Add(inputTextBox);
                    this.Controls.Add(readOnlyTextBox);
                }
                else if (result == DialogResult.No)
                {
                    // Действия для создания новой книги
                    Book_form form = new Book_form(0, books, id, a_b);
                    form.DataUpdated += DataUpdated;
                    form.Show();
                }
            }
            else
            {
                // Действия для создания новой книги
                Book_form form = new Book_form(0, books, id, a_b);
                form.DataUpdated += DataUpdated;
                form.Show();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                if (id != 0)
                {
                    result = MessageBox.Show("Вы хотите удалить книгу только у автора?", "Выбор действия",
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        if (select == 0)
                        {
                            throw new Exception("Строка не выбрана");
                        }

                        // Запрос подтверждения у пользователя
                        result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
                                                              "Подтверждение удаления",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Выполняем удаление
                            books.DelDb(select);
                            select = 0;
                            DataUpdated();
                        }
                        else
                        {
                            // Пользователь отменил действие
                            MessageBox.Show("Удаление отменено.");
                        }
                    }
                    else if (result == DialogResult.Yes)
                    {
                        if (select == 0)
                        {
                            throw new Exception("Строка не выбрана");
                        }
                        a_b.DelDb(id, select);
                        DataUpdated();
                    }
                }
                else
                {
                    if (select == 0)
                    {
                        throw new Exception("Строка не выбрана");
                    }

                    // Запрос подтверждения у пользователя
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
                                                          "Подтверждение удаления",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Выполняем удаление
                        books.DelDb(select);
                        select = 0;
                        DataUpdated();
                    }
                    else
                    {
                        // Пользователь отменил действие
                        MessageBox.Show("Удаление отменено.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Authors_Click(object sender, EventArgs e)
        {
            try
            {
                if (select == 0)
                {
                    throw new Exception("Строка не выбрана");
                }
                Authors_form form = new Authors_form(select);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
