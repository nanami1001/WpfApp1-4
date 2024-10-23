using System.Text;  // 引入 System.Text 命名空間，用於字串處理等
using System.Windows;  // 引入 System.Windows 命名空間，提供 WPF 功能
using System.Windows.Controls;  // 引入 System.Windows.Controls 命名空間，包含 WPF 控件
using System.Windows.Data;  // 引入 System.Windows.Data 命名空間，用於數據綁定
using System.Windows.Documents;  // 引入 System.Windows.Documents 命名空間，用於處理文檔內容
using System.Windows.Input;  // 引入 System.Windows.Input 命名空間，提供輸入事件處理
using System.Windows.Media;  // 引入 System.Windows.Media 命名空間，提供顏色和圖形支持
using System.Windows.Media.Imaging;  // 引入 System.Windows.Media.Imaging 命名空間，用於圖像處理
using System.Windows.Navigation;  // 引入 System.Windows.Navigation 命名空間，用於導航功能
using System.Windows.Shapes;  // 引入 System.Windows.Shapes 命名空間，提供圖形形狀支持

namespace test3  // 定義命名空間 test3
{  /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
    public partial class MainWindow : Window  // 定義 MainWindow 類，繼承自 Window
    {
        // 定義飲料名稱及其價格的字典
        Dictionary<string, int> drinks = new Dictionary<string, int>
        {
            { "紅茶大杯", 60 },
            { "紅茶小杯", 40 },
            { "綠茶大杯", 50 },
            { "綠茶小杯", 30 },
            { "可樂大杯", 50 },
            { "可樂小杯", 30 },
            { "咖啡大杯", 80 },
            { "咖啡小杯", 50 }
        };

        Dictionary<string, int> orders = new Dictionary<string, int>();  // 定義訂單字典，用於存儲用戶訂購的飲料
        string takeout = "";  // 定義外帶選項的變數

        public MainWindow()  // MainWindow 的建構函式
        {
            InitializeComponent();  // 初始化 WPF 控件

            // 顯示飲料品項
            DisplayDrinkMenu(drinks);  // 調用方法顯示飲料選單
        }

        private void DisplayDrinkMenu(Dictionary<string, int> drinks)  // 顯示飲料選單的方法
        {
          
            stackpanel_DrinkMenu.Height = 42 * drinks.Count;  // 根據飲料數量設定 StackPanel 的高度
            foreach (var drink in drinks)  // 遍歷每一種飲料
            {
                var sp = new StackPanel  // 創建新的 StackPanel 來顯示每種飲料
                {
                    Orientation = Orientation.Horizontal,  // 設定堆疊方向為水平
                    Margin = new Thickness(3),  // 設定邊距
                    Background = Brushes.LightBlue,  // 設定背景顏色為淺藍色
                    Height = 35,  // 設定高度
                };

                var cb = new CheckBox  // 創建一個勾選框
                {
                    Content = drink.Key,  // 設定內容為飲料名稱
                    FontFamily = new FontFamily("微軟正黑體"),  // 設定字型
                    FontSize = 16,  // 設定字型大小
                    FontWeight = FontWeights.Bold,  // 設定字型為粗體
                    Foreground = Brushes.Blue,  // 設定字型顏色為藍色
                    Width = 150,  // 設定寬度
                    Margin = new Thickness(5),  // 設定邊距
                    VerticalContentAlignment = VerticalAlignment.Center,  // 設定垂直對齊
                };

                var lb_price = new Label  // 創建一個標籤來顯示價格
                {
                    Content = $"{drink.Value}元",  // 設定內容為價格
                    FontFamily = new FontFamily("微軟正黑體"),  // 設定字型
                    FontSize = 16,  // 設定字型大小
                    FontWeight = FontWeights.Bold,  // 設定字型為粗體
                    Foreground = Brushes.Green,  // 設定字型顏色為綠色
                    Width = 60,  // 設定寬度
                    VerticalContentAlignment = VerticalAlignment.Center,  // 設定垂直對齊
                };

                var sl = new Slider  // 創建一個滑動條
                {
                    Width = 150,  // 設定寬度
                    Minimum = 0,  // 設定最小值
                    Maximum = 10,  // 設定最大值
                    Value = 0,  // 設定初始值
                    Margin = new Thickness(5),  // 設定邊距
                    VerticalAlignment = VerticalAlignment.Center,  // 設定垂直對齊
                    IsSnapToTickEnabled = true,  // 啟用刻度對齊
                };

                var lb_amount = new Label  // 創建一個標籤來顯示數量
                {
                    Content = "0",  // 初始內容為 0
                    FontFamily = new FontFamily("微軟正黑體"),  // 設定字型
                    FontSize = 16,  // 設定字型大小
                    FontWeight = FontWeights.Bold,  // 設定字型為粗體
                    Foreground = Brushes.Red,  // 設定字型顏色為紅色
                    VerticalContentAlignment = VerticalAlignment.Center,  // 設定垂直對齊
                    Width = 50,  // 設定寬度
                };

                // 創建數據綁定，將滑動條的值綁定到數量標籤的內容
                Binding myBinding = new Binding("Value");  // 創建新的數據綁定
                myBinding.Source = sl;  // 設定綁定來源為滑動條
                lb_amount.SetBinding(ContentProperty, myBinding);  // 將標籤的內容綁定到滑動條的值

                // 將創建的控制項添加到 StackPanel 中
                sp.Children.Add(cb);  // 添加勾選框
                sp.Children.Add(lb_price);  // 添加價格標籤
                sp.Children.Add(sl);  // 添加滑動條
                sp.Children.Add(lb_amount);  // 添加數量標籤

                stackpanel_DrinkMenu.Children.Add(sp);  // 將 StackPanel 添加到飲料選單
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)  // 當單選按鈕被選中時的事件處理器
        {
            var rb = sender as RadioButton;  // 將發送者轉換為 RadioButton
            if (rb.IsChecked == true)  // 如果單選按鈕被選中
            {
                //MessageBox.Show(rb.Content.ToString());  // 顯示選中內容的消息框（已被註解掉）
                takeout = rb.Content.ToString();  // 將選中內容賦值給 takeout
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)  // 訂購按鈕的點擊事件處理器
        {
            // 確認訂購內容
            orders.Clear();  // 清空現有的訂單
            for (int i = 0; i < stackpanel_DrinkMenu.Children.Count; i++)  // 遍歷飲料選單中的所有項目
            {
                var sp = stackpanel_DrinkMenu.Children[i] as StackPanel;  // 將子項目轉換為 StackPanel
                var cb = sp.Children[0] as CheckBox;  // 獲取勾選框
                var drinkName = cb.Content.ToString();  // 獲取飲料名稱
                var sl = sp.Children[2] as Slider;  // 獲取滑動條
                var amount = (int)sl.Value;  // 獲取數量

                // 如果勾選框被選中且數量大於 0，則將飲料及其數量添加到訂單中
                if (cb.IsChecked == true && amount > 0) orders.Add(drinkName, amount);
            }

            // 顯示訂購內容
            string msg = "";  // 用於儲存訂單訊息
            string discount_msg = "";  // 用於儲存折扣訊息
            int total = 0;  // 初始化總金額

            msg += $"此次訂購為{takeout}，訂購內容如下：\n";  // 添加外帶資訊到訊息
            int num = 1;  // 用於編號
            foreach (var order in orders)  // 遍歷訂單
            {
                int subtotal = drinks[order.Key] * order.Value;  // 計算小計
                msg += $"{num}. {order.Key} x {order.Value}杯，小計{subtotal}元\n";  // 添加每項訂單到訊息
                total += subtotal;  // 累加總金額
                num++;  // 編號遞增
            }
            msg += $"總金額為{total}元";  // 添加總金額到訊息

            int sellPrice = total;  // 設定售價為原價
            // 檢查是否符合折扣條件
            if (total >= 500)
            {
                sellPrice = (int)(total * 0.8);  // 計算 8 折後的售價
                discount_msg = $"恭喜您獲得滿500元打8折優惠";  // 設定折扣訊息
            }
            else if (total >= 300)
            {
                sellPrice = (int)(total * 0.9);  // 計算 9 折後的售價
                discount_msg = $"恭喜您獲得滿300元打9折優惠";  // 設定折扣訊息
            }
            else
            {
                discount_msg = $"未達到任何折扣條件";  // 設定未符合折扣的訊息
            }
            msg += $"\n{discount_msg}，原價為{total}元，售價為 {sellPrice}元。";  // 添加折扣訊息和售價到總訊息

            ResultTextBlock.Text = msg;  // 將最終訊息顯示在結果文本框中
        }
    }
}