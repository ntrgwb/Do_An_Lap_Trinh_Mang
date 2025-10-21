using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        private FirebaseClient firebase;
        public Form1()
        {
            InitializeComponent();
            firebase = new FirebaseClient("https://thuyettrinhdemo-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nhập tên trước đã!");
                return;
            }

            // Ghi dữ liệu
            await firebase.Child("names").PostAsync(new { name });

            MessageBox.Show($"Đã lưu {name} lên Firebase!");
            txtName.Clear();

            await LoadData();
        }

        private async Task LoadData()
        {
            lstNames.Items.Clear();

            var data = await firebase.Child("names").OnceAsync<object>();
            foreach (var item in data)
            {
                dynamic d = item.Object;
                lstNames.Items.Add(d.name.ToString());
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
