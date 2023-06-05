namespace BTLWeb.Models.ViewModels
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public string ProductId { set; get; }
        public TDanhMucSp Product { set; get; }
        public int Quantity { set; get; }
    }
}
