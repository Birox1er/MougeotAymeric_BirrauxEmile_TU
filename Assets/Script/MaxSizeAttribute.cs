using System;

internal class MaxSizeAttribute : Attribute
{
    private int _size;

    public MaxSizeAttribute(int size)
    {
        this._size = size;
    }
}