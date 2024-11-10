namespace ShopingCart
{
    class Program
    {
        static void Main()
        {
            List<Product> listOfProducts = GetProducts();
            ShoppingCart ShoppingCart = new();
            bool isRunning = true;

            ShowProductList(listOfProducts);
            ShowCommandList();

            while (isRunning)
            {
                Console.Write("\nChoose a command:");
                string command = Console.ReadLine()!.ToLower();

                switch (command)
                {
                    case "add":
                        ShoppingCart.AddProductToCart(listOfProducts);
                        break;
                    case "remove":
                        ShoppingCart.RemoveProductFromCart(listOfProducts);
                        break;
                    case "show cart":
                        ShoppingCart.ShowShoppingCartItems();
                        break;
                    case "sum":
                        float sum = ShoppingCart.GetTheSumOfTheCart();
                        Console.WriteLine($"\nSum: {sum}");
                        break;
                    case "max":
                        ShoppingCart.FindTheMaxInTheCart();
                        break;
                    case "clear":
                        Console.Clear();
                        ShowProductList(listOfProducts);
                        ShowCommandList();
                        break;
                    default:
                        Console.WriteLine("\nCommand does not exist");
                        break;
                }

            }
        }
        static List<Product> GetProducts()
        {
            string fileName = "..\\..\\..\\Duomenys.txt";

            StreamReader data = new(fileName);
            List<Product> listOfProducts = new List<Product>();

            int lineCount = File.ReadLines(fileName).Count();

            for (int i = 0; i < lineCount; i++)
            {
                string[] foodStringLine = data.ReadLine()!.Split(' ');
                string foodName = foodStringLine[0];
                float foodPrice = float.Parse(foodStringLine[1]);

                Product foodItem = new(foodName, foodPrice);
                listOfProducts.Add(foodItem);
            }

            return listOfProducts;
        }
        static void ShowProductList(List<Product> listOfProducts)
        {
            for (int i = 0; i < listOfProducts.Count; i++)
            {
                Console.WriteLine($"{(i + 1).ToString() + '.',-3} {listOfProducts[i].Name + ':',-11} {listOfProducts[i].Price:F2}$");
            }
        }
        static void ShowCommandList()
        {
            Console.WriteLine("\n\"add\" - Adds products to the shopping cart.");
            Console.WriteLine("\"remove\" - Removes products from the shopping cart.");
            Console.WriteLine("\"show cart\" - Displays the items currently in the shopping cart.");
            Console.WriteLine("\"sum\" - Calculates and displays the total sum of all products in the shopping cart.");
            Console.WriteLine("\"max\" - Finds and displays the product with the highest price or value in the shopping cart.");
            Console.WriteLine("\"clear\" - Clears the terminal.\n");
        }
    }
    class ShoppingCart
    {
        private List<Product> shopingCart = [];
        public void AddProductToCart(List<Product> listOfProducts)
        {
            Console.Write("Pick a product (1,12):");
            try
            {
                int index = int.Parse(Console.ReadLine()!) - 1;
                shopingCart.Add(listOfProducts[index]);
                Console.WriteLine($"Added {listOfProducts[index].Name} to the cart");
            }
            catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }
        }
        public void RemoveProductFromCart(List<Product> listOfProducts)
        {
            ShowShoppingCartItems();

            if(shopingCart.Count == 0) { return;  }

            Console.Write($"\nPick an item to remove (int):");
            int index = int.Parse(Console.ReadLine()!) - 1;
            shopingCart.RemoveAt(index);
        }
        public void ShowShoppingCartItems()
        {
            try
            {
                if (shopingCart.Count == 0)
                {
                    Console.WriteLine("Shoping cart is empty");
                    return;
                }


                for (int i = 0; i < shopingCart.Count; i++)
                {
                    Console.WriteLine($"{(i + 1).ToString() + '.',-3} {shopingCart[i].Name + ':',-10} {shopingCart[i].Price:F2}$");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public float GetTheSumOfTheCart()
        {
            float sumOfTheCart = 0;

            foreach (Product product in shopingCart)
            {
                sumOfTheCart += product.Price;
            }
            return sumOfTheCart;
        }
        public void FindTheMaxInTheCart()
        {
            var priciestItem = shopingCart.OrderByDescending(item => item.Price).FirstOrDefault();
            if (priciestItem == null)
            {
                Console.WriteLine("The shopping cart is empty.");
                return;
            }
            Console.WriteLine($"The priciest item is {priciestItem.Name} costing {priciestItem.Price}");
        }
    }
    class Product(string name, float price)
    {
        private string _name = name;
        private float _price = price;
        public string Name { get { return _name; } set { _name = value; } }
        public float Price { get { return _price; } set { _price = value; } }
    }
}