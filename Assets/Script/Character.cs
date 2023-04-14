using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamagable
{
    public int HP => 1;
    public int MaxHP => 1;

    public void Damage(int amount)
    {
        // Anim
    }

    public void Damage(float amount)
    {
        throw new NotImplementedException();
    }

    public void Regen(int amount)
    {
    }
}

//public class Interrupteur : MonoBehaviour, IHealth
//{
//    public int HP => throw new NotImplementedException();

//    public int MaxHP => throw new NotImplementedException();

//    public void Damage(int amount)
//    {

//    }
//}

public class Animalerie
{
    public List<Animal> Animals { get; private set; } = new();
}

public abstract class Animal
{
    public virtual void Crier() { }
    public abstract void Die();
}
public abstract class Cat : Animal
{

}
public class SuperCat : Cat
{

    public override void Crier()
    {
        base.Crier();

        Debug.Log("SuperChat");
    }

    public override void Die()
    {
        throw new NotImplementedException();
    }
}


public class Character : MonoBehaviour, IDamagable
{

    [SerializeField] IDamagable _d;

    List<int> _loto = new() { 12, 34, 56, 78, 90 };
    public IReadOnlyCollection<int> Loto { get => _loto; }

    private void Start()
    {
        //Animal a = new Animal();
        //Animal chat = new Cat();
        Animal superChat = new SuperCat();

        //chat.Crier();
        superChat.Crier();

        //Rigidbody rb = GetComponent<Rigidbody>() ?? GetComponentInParent<Rigidbody>();
    }

    void Method()
    {
        Dictionary<int, int> t;
        // On doit pas
        //Loto.Add(12);
        //Loto.Clear();

        if (Loto is List<int>)
        {
            List<int> loto2 = Loto as List<int>;  // Gentil
            List<int> loto3 = (List<int>)Loto;   // Mechant

            (Loto as List<int>).Add(12);
            ((List<int>)Loto).Add(12);

        }

        // Mais on devrait faire ça
        foreach (var el in Loto)
        {
            Debug.Log(el);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Character>()?.Damage(12);
        //other.GetComponent<DestructibleObject>()?.Damage(12);

        
        var h = other.GetComponent<IDamagable>();
        h.Damage(12);




        if (h is Character)
        {

        }
        switch (h)
        {
            case Character character:
                character.name = "";
                break;
            case DestructibleObject desobject:

                break;
        }

    }

    public int HP => throw new NotImplementedException();

    public int MaxHP => throw new NotImplementedException();

    public void Damage(int amount)
    {
        throw new NotImplementedException();
    }
    public void Regen(int amount)
    {
        throw new NotImplementedException();
    }

    public void Damage(float amount)
    {
        throw new NotImplementedException();
    }
}

public interface IReadableHealth : IDamagable
{
    int HP { get; }
    int MaxHP { get; }
}

public interface IDamagable
{
    void Damage(int amount);
    void Damage(float amount);
}


class TMP : IDamagable
{
    public void Damage(int amount)
    {
        throw new NotImplementedException();
    }

    public void Damage(float amount)
    {
        throw new NotImplementedException();
    }
}