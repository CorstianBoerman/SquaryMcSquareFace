using System;
using System.Drawing;

namespace SquaryMcSquareFace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the matrix size!");

            int num = 0;

            bool isValid = false;
            while (!isValid) {
                Console.WriteLine("Please enter an odd number");
                var result = Console.ReadLine();

                num = Convert.ToInt32(result);

                if (num % 2 == 1) {
                    isValid = true;
                }
                if (num > 4095) {
                    isValid = false;
                    Console.WriteLine("Hey hold on there cowboy");
                }
            };

            Console.WriteLine($"Matrix size is {num}x{num}");

            int[,] matrix = new int[num, num];

            int x = ((num) / 2);
            int y = num - 1;
            int prev_x = x;
            int prev_y = y;

            int counter = 1;
            int upper = num * num;

            while (counter <= upper)
            {
                if (matrix[x, y] == 0)
                {
                    matrix[x, y] = counter;
                    counter++;

                    prev_x = x;
                    prev_y = y;

                    x = x < num - 1 ? x + 1 : 0;
                    y = y < num - 1 ? y + 1 : 0;
                }
                else
                {
                    x = prev_x;
                    y = prev_y - 1;
                }
            }

            using (var image = new Bitmap(num, num)) {
                var multiplier = 16777216 / upper;

                for (var i = 0; i < num; i++) {
                    for (var ii = 0; ii < num; ii++) {
                        var hex = (matrix[i, ii] * multiplier).ToString("X8");

                        var color = Color.FromArgb(
                            (byte)255,
                            (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16)),
                            (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16)),
                            (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16))
                        );

                        image.SetPixel(i, ii, color);
                    }
                }

                image.Save($"./{Guid.NewGuid().ToString()}.bmp");
            }
        }
    }
}
