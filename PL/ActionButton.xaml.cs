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

namespace PL
{
    /// <summary>
    /// Interaction logic for ActionButton.xaml
    /// </summary>
    public partial class ActionButton : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(String), typeof(ActionButton), new PropertyMetadata(default(String)));
        /// <summary>
        /// the text on the button
        /// </summary>
        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(String), typeof(ActionButton), new PropertyMetadata(default(String)));
        /// <summary>
        /// the icon in the button from material icons
        /// </summary>
        public String Icon
        {
            get { return (String)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State", typeof(String), typeof(ActionButton), new PropertyMetadata(default(String)));
        /// <summary>
        /// flat or origin
        /// </summary>
        public String State
        {
            get { return (String)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public new static readonly DependencyProperty StyleProperty = DependencyProperty.Register(
            "Style", typeof(object), typeof(ActionButton), new PropertyMetadata(default(object)));
        /// <summary>
        /// the material design style for button
        /// </summary>
        public new object Style
        {
            get { return (object)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(ActionButton), new PropertyMetadata(default(ICommand)));
        /// <summary>
        /// the command
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(ActionButton), new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public event EventHandler ButtonClick;
        /// <summary>
        /// the click event
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">event arguments</param>
        protected void Button_Click(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            ButtonClick?.Invoke(this, e);
        }

        public ActionButton()
        {
            InitializeComponent();
            DataContext = this;

        }
    }
}
