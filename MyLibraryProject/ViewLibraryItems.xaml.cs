using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using MyLibraryProject;
using System.Data;
using System.Configuration;


namespace MyLibraryProject
{
    /// <summary>
    /// Interaction logic for ViewLibraryItems.xaml
    /// </summary>
    public partial class ViewLibraryItems : Window
    {
        public ViewLibraryItems()
        {
            InitializeComponent();
        }


        string MydbConnect = @"Data Source =.\SQLEXPRESS;Initial Catalog = MyLibrary; Integrated Security = True";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         //   LoadDataGrid();
        }
        
        void LoadDataGrid()
        {
            SqlConnection MysqlConnect = new SqlConnection(MydbConnect);
            try
            {
                MysqlConnect.Open();
                string MyLoginQuery = "select * from Library_Description";
                SqlCommand MyCommande = new SqlCommand(MyLoginQuery, MysqlConnect);
                MyCommande.ExecuteNonQuery();
                SqlDataAdapter MyDtAdpt = new SqlDataAdapter(MyCommande);
                DataTable MyDtTbl = new DataTable("Library_Description");
                MyDtAdpt.Fill(MyDtTbl);
                library_DescriptionDataGrid.ItemsSource = MyDtTbl.DefaultView;
                MyDtAdpt.Update(MyDtTbl);
                MysqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextSelection_TextChanged(object sender, TextChangedEventArgs e)
        {
               if (CBFillType.Text == "DVD" || CBFillType.Text == "Magazine" || CBFillType.Text == "Book")
                    {
                        SqlConnection con = new SqlConnection(MydbConnect);
                        SqlDataAdapter sda = new SqlDataAdapter("select * from Library_Description where Title like '" + TextSelection.Text + "%' and Type like '"+ CBFillType.Text+"%'",con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        library_DescriptionDataGrid.DataContext = dt;
                    }
                else if (CBFillType.Text == "All items")
                    {
                        SqlConnection con = new SqlConnection(MydbConnect);
                        SqlDataAdapter sda = new SqlDataAdapter("select * from Library_Description where Title like '" + TextSelection.Text + "%'", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        library_DescriptionDataGrid.DataContext = dt;
                    }
        }

        private void Bt_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

         private void Bt_Print_Click(object sender, RoutedEventArgs e)
       {
            // DGVPrinter printer = new DGVPrinter();

            PrintDialog print = new PrintDialog();
            if (print.ShowDialog()== true)
            {
                print.PrintVisual(library_DescriptionDataGrid, "Printing in process");
            }
       }


        /*  private void LoadViewItems()
       {
           MyLibraryEntities LE = new MyLibraryEntities();
           var data = from dt in LE.Library_Description select dt;
           library_DescriptionDataGrid.ItemsSource = data.ToList();
       }
        */

    }
}

