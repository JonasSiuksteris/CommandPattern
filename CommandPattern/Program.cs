using System;
using CommandPattern.Commands;
using CommandPattern.Repositories;

namespace CommandPattern
{
    class Program
    {
        static void Main()
        {
            var commandManager = new CommandManager();

            var shoppingCartRepository = new ShoppingCartRepository();
            var productRepository = new ProductsRepository();

            var product1 = productRepository.FindBy("ATOMOSNV");
            var product2 = productRepository.FindBy("SM7B");
            
            var addToCartCommand = new AddToCardCommand(
                shoppingCartRepository,
                productRepository,
                product1);

            var addToCartCommand2 = new AddToCardCommand(
                shoppingCartRepository,
                productRepository,
                product2);

            commandManager.Invoke(addToCartCommand);
            commandManager.Invoke(addToCartCommand2);

            PrintCart(shoppingCartRepository);
        }

        private static void PrintCart(ShoppingCartRepository shoppingCartRepository)
        {
            var totalPrice = 0m;
            foreach (var lineItem in shoppingCartRepository.LineItems)
            {
                var price = lineItem.Value.Product.Price * lineItem.Value.Quantity;

                Console.WriteLine($"{lineItem.Key} " +
                                  $"${lineItem.Value.Product.Price} x {lineItem.Value.Quantity} = ${price}");

                totalPrice += price;
            }

            Console.WriteLine($"Total price:\t${totalPrice}");
        }
    }
}
