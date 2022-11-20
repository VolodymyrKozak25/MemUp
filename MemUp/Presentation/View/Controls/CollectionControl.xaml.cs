using Business_Logic.Repositories;
using Database_Access_Level;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.View.Controls
{
    /// <summary>
    /// Interaction logic for CollectionControl.xaml
    /// </summary>
    public partial class CollectionControl : UserControl
    {

        public Collection Collection
        {
            get { return (Collection)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Collection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionProperty =
            DependencyProperty.Register("Collection", typeof(Collection), typeof(CollectionControl), new PropertyMetadata(new Collection(), SetCollectionData));


        public int ReviewCount
        {
            get { return (int)GetValue(ReviewCountProperty); }
            set { SetValue(ReviewCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Collection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewCountProperty =
            DependencyProperty.Register("ReviewCount", typeof(int), typeof(CollectionControl), new PropertyMetadata(0, SetReviewCount));

        public int StudyCount
        {
            get { return (int)GetValue(StudyCountProperty); }
            set { SetValue(StudyCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Collection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StudyCountProperty =
            DependencyProperty.Register("StudyCount", typeof(int), typeof(CollectionControl), new PropertyMetadata(0, SetStudyCount));


        private static void SetStudyCount(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CollectionControl;
            var studyCount = e.NewValue as int?;


            if (control != null)
            {
                using (var unitOfWork = new UnitOfWork(new MemUpDBContext()))
                {
                    control.collectionStudyLabel.Content = studyCount;
                }
            }
        }

        private static void SetReviewCount(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CollectionControl;
            var reviewCount = e.NewValue as int?;


            if (control != null)
            {
                using (var unitOfWork = new UnitOfWork(new MemUpDBContext()))
                {
                    control.collectionReviewLabel.Content = reviewCount;
                }
            }
        }

        private static void SetCollectionData(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CollectionControl;
            var collection = e.NewValue as Collection;

            if (control != null)
            {
                control.collectionNameLabel.Content = collection.CollectionName;
            }
        }

        public CollectionControl()
        {
            InitializeComponent();
        }
    }
}
