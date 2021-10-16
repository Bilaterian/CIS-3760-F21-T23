using System.Collections.Generic;

public struct SerializableListContainer<T>
{
    public List<T> list;

    public SerializableListContainer(List<T> dataList)
    {
        this.list = dataList;
    }
}