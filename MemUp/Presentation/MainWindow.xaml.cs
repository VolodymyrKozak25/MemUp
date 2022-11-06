using Business_Logic.Repositories;
using Database_Access_Level;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;

        public MainWindow()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork(new MemUpDBContext());
        }

        private void CloseWindow(object sender, System.EventArgs e)
        {
            _unitOfWork.Dispose();
            Close();
        }
    }
}
