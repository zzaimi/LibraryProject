using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MyLibraryProject
{
    /// <summary>
    /// Interaction logic for InsertLibraryItems.xaml
    /// </summary>
    /// 
    public partial class InsertLibraryItems : Window

    {
        public InsertLibraryItems()

        {
            InitializeComponent();
            FillSelectTitle();
        }

        string MydbConnect = @"Data Source =.\SQLEXPRESS;Initial Catalog = MyLibrary; Integrated Security = True";

        void FillSelectTitle()
        {
            SqlConnection MysqlConnect = new SqlConnection(MydbConnect);
            try
            {
                MysqlConnect.Open();
                string MyLoginQuery = "select * from Library_Description";
                SqlCommand MyCommande = new SqlCommand(MyLoginQuery, MysqlConnect);
                SqlDataReader dr = MyCommande.ExecuteReader();

                while (dr.Read())
                {
                    // insert argument 3 (the column num of title on the table)
                    string Title = dr.GetString(3);
                    CBSelectTitle.Items.Add(Title);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bt_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Hide();
        }

        private void ResetAll()
        {
            IDText.Text = "";
            TypeText.Text = "";
            CategoryText.Text = "";
            TitleText.Text = "";
            AutorText.Text = "";
            DateText.SelectedDate = null;
            VersionText.Text = "";
            DescriptionText.Text = "";

            Bt_Insert.IsEnabled = true;
            Bt_Update.IsEnabled = false;
            Bt_Delete.IsEnabled = false;
        }


        private void IUD(int state)
        {
            SqlConnection MysqlConnect = new SqlConnection(MydbConnect);
            try
            {
                MysqlConnect.Open();
                switch (state)
                {
                    case 0:
                        string MyLoginQueryinsert = "insert into Library_Description (ID,Type,Category,Title,Date,Version,Autor,Descr) values \n" +
                        "('" + IDText.Text + "','" + TypeText.Text + "','" + CategoryText.Text + "','" + TitleText.Text + "','" + DateText.Text + "'\n" +
                        ",'" + VersionText.Text + "','" + AutorText.Text + "','" + DescriptionText.Text + "')";
                       
                         SqlCommand MyCommandeinsert = new SqlCommand(MyLoginQueryinsert, MysqlConnect);
                         //execute the query
                         MyCommandeinsert.ExecuteNonQuery();
                         MessageBox.Show ("Item insered successfully!");
                         break;

                    case 1:
                        string MyLoginQueryupdate = "update Library_Description set ID= '" + IDText.Text + "',Type='" + TypeText.Text + "',Category='" + CategoryText.Text + "',\n" +
                        "Title='" + TitleText.Text + "',Date='" + DateText.Text + "',Version='" + VersionText.Text + "',Autor='" + AutorText.Text + "',\n" +
                        "Descr='" + DescriptionText.Text + "' where ID= '" + IDText.Text + "'";
                        SqlCommand MyCommandeupdate = new SqlCommand(MyLoginQueryupdate, MysqlConnect);
                        //execute the query
                        MyCommandeupdate.ExecuteNonQuery();
                        MessageBox.Show("Item updates successfully!");
                        break;

                    case 2:
                        string MyLoginQuerydelete = "delete from Library_Description where ID= '" + IDText.Text + "'";
                        SqlCommand MyCommandedelete = new SqlCommand(MyLoginQuerydelete, MysqlConnect);
                        //execute the query
                        MyCommandedelete.ExecuteNonQuery();
                        MessageBox.Show("Item deleted successfully!");
                        break;
                }
                MysqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void Bt_Insert_Click(object sender, RoutedEventArgs e)
              {
            this.IUD(0);
            this.ResetAll();
        }

        private void Bt_Update_Click(object sender, RoutedEventArgs e)
              {
                this.IUD(1);
              }

              private void Bt_Delete_Click(object sender, RoutedEventArgs e)
              {
            this.IUD(2);
            this.ResetAll();
        }

     
            private void CBSelectTitle_DropDownClosed(object sender, EventArgs e)
        {
            SqlConnection MysqlConnect = new SqlConnection(MydbConnect);
            try
            {
                MysqlConnect.Open();
                string MyLoginQuery = "select * from Library_Description where Title='" + CBSelectTitle.Text + "'";
                SqlCommand MyCommande = new SqlCommand(MyLoginQuery, MysqlConnect);
                SqlDataReader dr = MyCommande.ExecuteReader();

                while (dr.Read())
                {
                    string VarID = dr.GetString(0);
                    string VarType = dr.GetString(1).ToString();
                    string VarCategory = dr.GetString(2).ToString();
                    string VarTitle = dr.GetString(3);
                    string VarDate = dr.GetDateTime(4).ToString();
                    string VarVersion = dr.GetString(5);
                    string VarAutor = dr.GetString(6);
                    string VarDescription = dr.GetString(7);

                    IDText.Text = VarID;
                    TypeText.Text = VarType;
                    CategoryText.Text = VarCategory;
                    TitleText.Text = VarTitle;
                    DateText.Text = VarDate;
                    VersionText.Text = VarVersion;
                    AutorText.Text = VarAutor;
                    DescriptionText.Text = VarDescription;

                    Bt_Update.IsEnabled = true;
                    Bt_Delete.IsEnabled = true;
                  //  CBSelectTitle.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CBSelectTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSelectTitle.Items.Refresh();
        }

        private void Bt_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.ResetAll();
        }
    }
}
