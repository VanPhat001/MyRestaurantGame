public class FoodPool : BasePool<FoodPool.FoodName>
{
    public enum FoodName
    {
        Pizza,
        Sandwich
    }


    public static FoodPool Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
    }



    protected override bool Compare(FoodName a, FoodName b)
    {
        return a == b;
    }

    protected override bool Compare(FoodName a, string b)
    {
        return a.ToString() == b;
    }

}