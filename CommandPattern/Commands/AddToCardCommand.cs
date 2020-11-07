using CommandPattern.Models;
using CommandPattern.Repositories;

namespace CommandPattern.Commands
{
    public class AddToCardCommand : ICommand
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly Product _product;

        public AddToCardCommand(
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            Product product)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _product = product;
        }

        public void Execute()
        {
            if (_product == null) return;

            _productRepository.DecreaseStockBy(_product.ArticleId, 1);

            _shoppingCartRepository.Add(_product);
        }

        public bool CanExecute()
        {
            if (_product == null) return false;

            return _productRepository.GetStockFor(_product.ArticleId) > 0;

        }

        public void Undo()
        {
            if (_product == null) return;

            var itemQuantity = _shoppingCartRepository.Get(_product.ArticleId).Quantity;

            _productRepository.IncreaseStockBy(_product.ArticleId, itemQuantity);

            _shoppingCartRepository.RemoveAll(_product.ArticleId);
        }
    }
}
