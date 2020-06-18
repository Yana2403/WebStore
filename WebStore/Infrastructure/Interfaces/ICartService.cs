using WebStore.ViewModel;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int id);

        void DecrementFromCart(int id); //уменьшить кол-во товаров в корзине

        void RemoveFromCart(int id);

        void RemoveAll();

        CartViewModel TransformFromCart();
    }
}
