using System;
using System.Windows.Forms;
using System.Data.SQLite;
using MaterialSkin.Controls;
using WinForms_ToDoList.Forms;
using WinForms_ToDoList.Database;


namespace WinForms_ToDoList
{
    public partial class MainForm : MaterialForm
    {
        #region Глобальные переменные
        int openForm = 0;
        

        SQLiteConnection connection = new SQLiteConnection(DbConnection.dbConnection);
        #endregion

        public MainForm()
        {
            InitializeComponent();

            

            DeadLineToDoLabel.Text = DateTime.Now.ToShortDateString();
            DoneText.Text = "Нет";
        }       

       

        #region Открытие окна с ифнормации о приложении с дополнительной проверкой
        private void AboutProgramButton_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "AboutForm")
                    openForm = 1;
                else
                    openForm = 0;
            }
          
            
        }
        #endregion

        #region Подтверждение закрытия приложения
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageExitForm exitForm = new MessageExitForm();
            exitForm.ShowDialog();

            if (exitForm.DialogResult == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        #endregion

        #region Выбор даты выполнения
        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            DeadLineToDoLabel.Text = "";
            DeadLineToDoLabel.Text = e.Start.ToShortDateString();
        }
        #endregion

        #region Загрузка данных
        private void MainForm_Load(object sender, EventArgs e)
        {
            connection.Open();
            DbController.LoadData(ToDoDataGridView);
            connection.Close();
        }
        #endregion

        #region Обработчики событий

        #region Добавление новых данных в базу
        private void AddDataButton_Click(object sender, EventArgs e)
        {
            DbController.AddNewData(DeadLineToDoLabel, ToDoText, DoneText, ToDoDataGridView);
        }
        #endregion

        #region Редактирование данных
        private void EditDataButton_Click(object sender, EventArgs e)
        {
            DbController.EditData(ToDoDataGridView, DeadLineToDoLabel, ToDoText, DoneText);
        }
        #endregion

        #region Удаление данных
        private void DeleteDataButton_Click(object sender, EventArgs e)
        {
            DbController.DeleteData(ToDoDataGridView, DeadLineToDoLabel, ToDoText, DoneText);
        }
        #endregion

        #region Эспорт данных
        private void ExportDataButton_Click(object sender, EventArgs e)
        {
            DbController.ExportDataExcel(ToDoDataGridView);
        }

        #endregion

        #endregion

        #region Вывод сохранённых данных в поля
        private void ToDoDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in ToDoDataGridView.SelectedRows)
            {
                DeadLineToDoLabel.Text = ToDoDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                ToDoText.Text = ToDoDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                DoneText.Text = ToDoDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            }
        }
        #endregion       
    }
}
