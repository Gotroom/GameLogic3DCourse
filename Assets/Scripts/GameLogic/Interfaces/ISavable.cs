using System.Collections.Generic;


public interface ISavable<T>
{
    void Save(T data, string path = null);
    void Save(List<T> data, string path = null);
    T Load(string path = null);
    List<T> LoadList(string path = null);
}
