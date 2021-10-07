using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log
{
    public partial class Form3 : Form
    {
        private static int quantity_te1 { get; set; } = 0;
        private static int quantity_te2 { get; set; } = 0;
        private static int quantity_te3 { get; set; } = 0;
        private static int quantity_te5 { get; set; } = 0;
        private static int quantity_te6 { get; set; } = 0;
        private static double quantity_K_nk { get; set; } = 0;
        private static double quantity_K_mich { get; set; } = 0;
        private static double quantity_K_krk { get; set; } = 0;
        private static double quantity_K_m { get; set; } = 0;
        private static int[,] global_a1 { get; set; } = new int[1, 1];

        public Form3(TextBox[,] T_Array, int Count)
        {
            InitializeComponent();
            int[,] T_Arr = new int[2, Count];
            for (int i = 0; i < 2; i++)
            {
                for (int g = 0; g < Count; g++)
                {
                    T_Arr[i, g] = int.Parse(T_Array[i, g].Text);
                }
            }
            int[,] A1 = Trans(T_Arr);
            global_a1 = A1;
            Matrix_X(A1, A1.GetUpperBound(0) + 1);
            Te1(T_Arr, Count);
            Te2(T_Arr, Count);
            Te3(T_Arr, Count);
            K_nk(Count);
            K_krk();
            K_m(T_Arr);
            textBox1.Text = quantity_te1.ToString();
            textBox2.Text = quantity_te2.ToString();
            textBox3.Text = quantity_te3.ToString();
            textBox4.Text = quantity_te5.ToString();
            textBox5.Text = quantity_te6.ToString();
            textBox6.Text = quantity_K_nk.ToString();
            textBox7.Text = quantity_K_mich.ToString();
            textBox8.Text = quantity_K_krk.ToString();
            textBox9.Text = quantity_K_m.ToString();
        }
        public int[,] Matrix_X(int[,] matrix, int rows)
        {
            int[,] Multiplied_A = new int[rows, rows];
            int[,] Multiplied_Zero = new int[rows, rows];
            int[,] Multiplied_B = matrix;
            int checker = 1;
            int index_of = 1;
            Te4(Multiplied_B, index_of);
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, i] == 1)
                {
                    throw new Exception($"Matrix-i {index_of} astichani ankyunagcum ka 1, ays depqum klini anverch bazmapatkum");
                }
            }
            while (checker != 0)
            {
                checker = 0;
                for (int i = 0; i < rows; i++)
                {
                    if (Multiplied_B[i, i] == 1)
                    {
                        throw new Exception($"Matrix-i {index_of} astichani ankyunagcum ka 1, ays depqum klini anverch bazmapatkum");
                    }
                }
                for (int i = 0; i < rows; i++)
                {

                    for (int j = 0; j < rows; j++)
                    {
                        Multiplied_A[i, j] = 0;
                        for (int h = 0; h < rows; h++)
                        {
                            Multiplied_A[i, j] += Multiplied_B[i, h] * matrix[h, j];
                        }
                        checker += Multiplied_A[i, j];
                    }
                }
                index_of++;
                Te4(Multiplied_A, index_of);
                Multiplied_B = Multiplied_A;
                Multiplied_A = Multiplied_Zero;
            }
            quantity_K_mich /= (double)index_of;
            return Multiplied_A;
        }
        private void K_m(int[,] Array)
        {
            int[] arr = Array.Cast<int>().ToArray();
            double maxValue = arr.Max();
            double t2 = quantity_te2;
            quantity_K_m = t2 / maxValue;
        }
        private void Te1(int[,] T_Arr, int Count)
        {
            int[] T1_Arr = new int[Count];
            int checker = 0;
            for (int g = 0; g < Count; g++)
            {
                checker = 0;
                for (int i = 0; i < Count; i++)
                {
                    if (T_Arr[0, g] == T_Arr[1, i])
                    {
                        checker++;
                        break;
                    }
                }
                if (checker == 0)
                {
                    T1_Arr[g] = T_Arr[0, g];
                }
            }
            int[] T1_Main_Array = Te_Drawer(T1_Arr);
            quantity_te1 = T1_Main_Array.Length;

        }
        private void Te2(int[,] T_Arr, int Count)
        {
            int[] T2_Arr = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                for (int g = 0; g < Count; g++)
                {
                    if (T_Arr[0, i] == T_Arr[1, g] && T_Arr[1, i] != 0)
                    {
                        T2_Arr[i] = T_Arr[0, i];
                        break;
                    }
                }
            }
            int[] T2_Main_Array = Te_Drawer(T2_Arr);
            Te5(T2_Main_Array);
            quantity_te2 = T2_Main_Array.Length;
        }
        private void Te3(int[,] T_Arr, int Count)
        {
            int[] Te3_Temp = new int[Count];
            int f = 0;
            int quantity_local = 0;
            for (int i = 0; i < Count; i++)
            {
                for (int g = 0; g < Count; g++)
                {
                    if (i == g && T_Arr[1, g] == 0)
                    {
                        Te3_Temp[i] = T_Arr[0, i];
                        quantity_local++;
                        break;
                    }
                }
            }
            int[] Te3_Main = new int[quantity_local];
            foreach (var item in Te3_Temp)
            {
                if (item != 0)
                {
                    Te3_Main[f] = item;
                    f++;
                }
            }
            quantity_te3 = quantity_local;
            Te6(T_Arr, Count, Te3_Main);
        }
        public void Te4(int[,] matrix, int index)
        {
            int t4_checker = 0;
            int quantity = 0;
            for (int i = 0; i < matrix.GetUpperBound(1) + 1; i++)
            {
                for (int g = 0; g < matrix.GetUpperBound(0) + 1; g++)
                {
                    t4_checker += matrix[g, i];
                    if (t4_checker != 0)
                    {
                        break;
                    }
                }
                if (t4_checker == 0)
                {
                    quantity++;
                }
                t4_checker = 0;
            }
            Te7(quantity, index);
        }
        private void Te5(int[] T2_Arr)
        {
            int local_quantity = 0;
            for (int i = 0; i < T2_Arr.Length; i++)
            {
                for (int j = 0; j < T2_Arr.Length; j++)
                {
                    if (global_a1[T2_Arr[j] - 1,T2_Arr[i]-1] == 1)
                    {
                        local_quantity++;
                    }
                }
            }
            quantity_te5 = local_quantity;
        }
        private void Te6(int[,] T_Arr, int Count, int[] T3_Arr)
        {
            int local_quantity = 0;
            for (int i = 0; i < T3_Arr.Length; i++)
            {
                for (int g = 0; g < Count; g++)
                {
                    if (T3_Arr[i] == T_Arr[0, g])
                    {
                        foreach (var item in T3_Arr)
                        {
                            if (item == T_Arr[1, g])
                            {
                                local_quantity++;
                                break;
                            }
                        }
                    }
                }
            }
            quantity_te6 = local_quantity;
        }
        public void Te7(int T_4, int index)
        {
            double T7 = T_4 - index;
            quantity_K_mich += T7 / (double)T_4;
        }
        private void K_nk(int Count)
        {
            double te5 = quantity_te5;
            double te3 = quantity_te3;
            double count = Count;
            quantity_K_nk = te5 / (count - te3);
        }
        private void K_krk()
        {
            double t6 = quantity_te6;
            double t3 = quantity_te3;
            quantity_K_krk = (2.0 * t6) / (t3 * (t3 - 1.0));
        }
        private int[,] Trans(int[,] log)
        {
            int[] temp = log.Cast<int>().ToArray();
            int maxValue = temp.Max();
            int[,] temp_arr = new int[maxValue, maxValue];
            try
            {
                for (int i = 0; i < log.GetUpperBound(1); i++)
                {
                    if (log[1, i] - 1 < 1)
                    {
                        continue;
                    }
                    temp_arr[log[0, i] - 1, log[1, i] - 1] = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return temp_arr;
        }
        private int[] Te_Drawer(int[] T_Temp_Array)
        {
            int f = 0;
            int l = 0;
            int quantity_local = 0;
            foreach (var item in T_Temp_Array)
            {
                if (item != 0)
                {
                    quantity_local++;
                }
            }
            int[] T_Temp2_Array = new int[quantity_local];
            foreach (var num in T_Temp_Array)
            {
                if (num != 0)
                {
                    T_Temp2_Array[f] = num;
                    f++;
                }
            }
            for (int i = 0; i < T_Temp2_Array.Length; i++)
            {
                int count = 0;
                for (int g = i ; g < T_Temp2_Array.Length; g++)
                {
                    if (T_Temp2_Array[i] == T_Temp2_Array[g] && i != g && g > i)
                        count++;
                }
                if (count == 0)
                {
                    l++;
                }
            }
            int[] T_Main_Array = new int[l];
            int c = 0;
            for (int i = 0; i < T_Temp2_Array.Length; i++)
            {
                int count = 0;
                for (int g = 0; g < T_Temp2_Array.Length; g++)
                {
                    if (T_Temp2_Array[i] == T_Temp2_Array[g] && i != g)
                    {
                        if (T_Temp2_Array[i] == T_Temp2_Array[g] && i != g && g > i)
                            count++;
                    }
                }
                if (count == 0)
                {
                    T_Main_Array[c] = T_Temp2_Array[i];
                    c++;
                }
            }
            return T_Main_Array;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (quantity_K_m >= 0 && quantity_K_m < 0.5)
            {
                MessageBox.Show(
            $"Միջանկյալ տարրերի գործակիցը՝ K_m = {quantity_K_m}, նշանակում է, որ համակարգի կառուցվածքի բարդությունը ցածր աստիճանի է",
            "Կառուցվածք",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_m >= 0.5 && quantity_K_m < 0.6)
            {
                MessageBox.Show(
            $"Միջանկյալ տարրերի գործակիցը՝ K_m = {quantity_K_m}, նշանակում է, որ համակարգի կառուցվածքի բարդությունը միջին աստիճանի է",
            "Կառուցվածք",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_m >= 0.6 && quantity_K_m <= 1)
            {
                MessageBox.Show(
            $"Միջանկյալ տարրերի գործակիցը՝ K_m = {quantity_K_m}, նշանակում է, որ համակարգի կառուցվածքի բարդությունը բարձր աստիճանի է",
            "Կառուցվածք",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Math.Abs(quantity_K_m - quantity_K_mich) >= 0.01)
            {
                MessageBox.Show(
            $"| k_m - k_mich | ({Math.Abs(quantity_K_m - quantity_K_mich)}) >=  0․01, նշանակում է համակարգում փաստաթղթաշրջանառության գործընթացը կազմակերպված է ոչ ռացիոնալ",
            "Ռացիոնալություն",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (Math.Abs(quantity_K_m - quantity_K_mich) <= 0.01 )
            {
                MessageBox.Show(
            $"| k_m - k_mich | ({Math.Abs(quantity_K_m - quantity_K_mich)}) <=  0․01, նշանակում է համակարգում փաստաթղթաշրջանառության գործընթացը կազմակերպված ռացիոնալ",
            "Ռացիոնալություն",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (quantity_K_nk >= 0 && quantity_K_nk < 0.5)
            {
                MessageBox.Show(
            $"Ներքին կապերի գործակիցը՝ K_nk = {quantity_K_nk}, նշանակում է, որ համակարգում ներքին կապակցվածության աստիճանը ցածր է",
            "Ներքին կապեր",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_nk >= 0.5 && quantity_K_nk < 0.6)
            {
                MessageBox.Show(
            $"Ներքին կապերի գործակիցը՝ K_nk = {quantity_K_nk}, նշանակում է, որ համակարգում ներքին կապակցվածության աստիճանը միջին է",
            "Ներքին կապեր",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_nk >= 0.6 && quantity_K_nk <= 1)
            {
                MessageBox.Show(
            $"Ներքին կապերի գործակիցը՝ K_nk = {quantity_K_nk}, նշանակում է, որ համակարգում ներքին կապակցվածության աստիճանը բարձր է",
            "Ներքին կապեր",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (quantity_K_krk >= 0 && quantity_K_krk < 0.5)
            {
                MessageBox.Show(
            $"Կրկնման գործակիցը՝ K_krk = {quantity_K_krk}, նշանակում է, որ համակարգում ելքային ինֆորմացիայի ավելցուկությունը ցածր է",
            "Կրկնման գործակից",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_krk >= 0.5 && quantity_K_krk < 0.6)
            {
                MessageBox.Show(
            $"Կրկնման գործակիցը՝ K_krk = {quantity_K_krk}, նշանակում է, որ համակարգում ելքային ինֆորմացիայի ավելցուկությունը միջին է",
            "Կրկնման գործակից",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
            if (quantity_K_krk >= 0.6 && quantity_K_krk <= 1)
            {
                MessageBox.Show(
            $"Կրկնման գործակիցը՝ K_krk = {quantity_K_krk}, նշանակում է, որ համակարգում ելքային ինֆորմացիայի ավելցուկությունը բարձր է",
            "Կրկնման գործակից",
            MessageBoxButtons.OK,
            MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
