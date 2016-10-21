using System;
using System.Windows;
using System.Windows.Controls;

namespace SR27_Desktop
{
    //1.1. Renamed to Shell
    //1.2. Added     xmlns:prism="http://www.codeplex.com/prism"
    //1.3.   prism:RegionManager.RegionName="Commands"
    //1.4.     prism:RegionManager.RegionName="Results"
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            new ThemesCombo(appTheme, this);
        }

    }

    public class ThemesCombo
    {
        Window Res = null;
        ComboBox appTheme = null;
        string[] themes = {
                 "ShinyRed",
                 "ShinyBlue",
                 "ExpressionLight",
                 //"ExpressionDark",
                 "aero.normalcolor",
                 "aero2.normalcolor",
                 "aerolite.normalcolor",
                 "classic",
                 "luna.homestead",
                 "luna.metallic",
                 "luna.normalcolor",
                 "royale.normalcolor"
        };
        public ThemesCombo(ComboBox cbx, Window res)
        {
            appTheme = cbx;
            Res = res;

            if (cbx == null || res == null)
            {
                throw new Exception("cbx cannot be null");
            }

            cbx.Items.Clear();
            foreach (string t in themes)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = t;
                cbx.Items.Add(cbi);
            }
          
            cbx.SelectionChanged += Cbx_SelectionChanged;
            cbx.SelectedIndex = 0;
        }

        private void Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeTheme();
        }


        private void changeTheme()
        {
            if (appTheme.SelectedItem == null)
                return;
            ComboBoxItem cbi = appTheme.SelectedItem as ComboBoxItem;
            string theme = (string)cbi.Content;
            string src = null;
            switch (theme)
            {
                case "ShinyRed":
                case "ShinyBlue":
                case "ExpressionLight":
                //case "ExpressionDark":

                    string dic = string.Format(@"Themes{0}{1}.xaml", '/', theme);
                    loadResDict(dic);
                    break;
                case "aero.normalcolor":
                    src = @"/PresentationFramework.Aero,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35;component/themes/aero.normalcolor.xaml";
                    break;

                case "aero2.normalcolor":
                    src = @"/presentationFramework.Aero2,Version=4.0.0.0, PublicKeyToken=31bf3856ad364e35,Culture=neutral;component/themes/aero2.normalcolor.xaml";
                    break;
                case "aerolite.normalcolor":
                    src = @"/presentationFramework.AeroLite,Version=4.0.0.0,PublicKeyToken=31bf3856ad364e35,Culture=neutral;component/themes/aerolite.normalcolor.xaml";
                    break;
                case "classic":

                    src = @"/presentationFramework.Classic,Version=4.0.0.0,PublicKeyToken=31bf3856ad364e35,Culture=neutral;component/themes/Classic.xaml";

                    break;
                case "luna.homestead":
                    src = @"/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35;component/themes/luna.homestead.xaml";
                    break;
                case "luna.metallic":
                    src = "/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35;component/themes/luna.metallic.xaml";
                    break;
                case "luna.normalcolor":
                    src = @"/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35;component/themes/luna.normalcolor.xaml";
                    break;
                case "royale.normalcolor":
                    src = @"/PresentationFramework.Royale,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35;component/themes/royale.normalcolor.xaml";
                    break;
                default:
                    break;

            }
            if (string.IsNullOrWhiteSpace(src) == false)
            {
                loadlib(src);
            }
        }


        private void loadlib(string src)
        {
            ResourceDictionary dic = new ResourceDictionary { Source = new Uri(src, UriKind.RelativeOrAbsolute) };
            Res.Resources.MergedDictionaries.Clear();
            Res.Resources.MergedDictionaries.Add(dic);
        }

        private void loadResDict(string StyleToUse)
        {

            ResourceDictionary dic = new ResourceDictionary { Source = new Uri(StyleToUse, UriKind.Relative) };
            Res.Resources.MergedDictionaries.Clear();
            Res.Resources.MergedDictionaries.Add(dic);

        }
    }
}
