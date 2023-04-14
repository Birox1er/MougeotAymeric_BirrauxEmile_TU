using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat acuelle de HP
        /// </summary>
        int _currentHealth;
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            MaxHealth = baseHealth;
            Attack = baseAttack;
            Defense = baseDefense;
            Speed = baseSpeed;
            BaseType = baseType;
            CurrentHealth=MaxHealth;
            IsAlive = true;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get=>_currentHealth; private set=>_currentHealth=value; }
        public TYPE BaseType { get => _baseType; private set => _baseType = value; }
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get => _baseHealth; private set => _baseHealth = value;

        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get => _baseAttack;private set => _baseAttack = value;
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get => _baseDefense; private set => _baseDefense = value;
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get => _baseSpeed; private set => _baseSpeed = value;
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive { get; private set; }

        
        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s,Character e)
        {
            if (s == null || e == null)
            {
                throw new ArgumentNullException();
            }
            s.PowerChange(TypeResolver.GetFactor(s.Type, BaseType));
           
            if (s.Power < 0)
            {
                CurrentHealth -= s.Power + s.Power * e.Attack / 200;
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
            }
            else
            {
                CurrentHealth -= Math.Max((s.Power + s.Power * e.Attack / 200 - Defense), 0);
                if (CurrentHealth <= 0)
                {
                    IsAlive = false;
                    CurrentHealth = 0;
                }
            }
            if (CurrentStatus == null)
            {
                CurrentStatus = StatusEffect.GetNewStatusEffect(s.Status);
            }
        }
        public void ReceiveAttack(Skill s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            s.PowerChange(TypeResolver.GetFactor(s.Type, BaseType));
            
            if (s.Power < 0)
            {
                CurrentHealth -= s.Power;
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
            }
            CurrentHealth -= Math.Max((s.Power - Defense), 0);
            if (CurrentHealth <= 0)
            {
                IsAlive = false;
                CurrentHealth = 0;
            }
            if (CurrentStatus == null)
            {
                CurrentStatus = StatusEffect.GetNewStatusEffect(s.Status);
            }
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            CurrentEquipment = newEquipment;
            MaxHealth += newEquipment.BonusHealth;
            Attack += newEquipment.BonusAttack;
            Defense += newEquipment.BonusDefense;
            Speed += newEquipment.BonusSpeed;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            if (CurrentEquipment == null)
            {
                throw new NullReferenceException();
            }
            MaxHealth -= CurrentEquipment.BonusHealth;
            Attack -= CurrentEquipment.BonusAttack;
            Defense -= CurrentEquipment.BonusDefense;
            Speed -= CurrentEquipment.BonusSpeed;
            CurrentEquipment = null;
        }
        public void Burn()
        {
            CurrentHealth -= 10;
        }
        public void Crazy()
        {
            CurrentHealth -= Convert.ToInt32(Attack * 0.3f);
        }
    }
}
