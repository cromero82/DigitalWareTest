using FacturaBL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacturaBL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseFacturasEntities db = new DatabaseFacturasEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //db.DOM_TEST
            DOM_TEST dbItem = new DOM_TEST();
            dbItem.nombre = txtNombreTest.Text;
            ///db.DOM_TEST.Add(new DOM_TEST { nombre = "XXXX" });
            //db.DOM_TEST.Attach(dbITEM) = ENTI
            db.Entry(dbItem).State = EntityState.Added;
            db.SaveChanges();

           // MessageBox.Show("ok");

            var lista = db.DOM_TEST.Select(item =>
            new TestViewModel
            {
                 id  = item.id,
                nombre = item.nombre
            }
            ).ToList<TestViewModel>();

        }
    }
}
