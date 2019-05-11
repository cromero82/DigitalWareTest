using FacturaBL.Admin;
using Modelos.Models;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace FacturaBL
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestBL BL = new TestBL();
        private DatabaseFacturasEntities db = new DatabaseFacturasEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var jresult = BL.GetListActivos();

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
