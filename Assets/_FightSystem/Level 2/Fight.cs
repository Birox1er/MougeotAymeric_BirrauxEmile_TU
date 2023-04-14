
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        Character _character1;
        Character _character2;
        public Fight(Character character1, Character character2)
        {
            if(character1==null|| character2 == null)
            {
                throw new ArgumentNullException();
            }
            Character1 = character1;
            Character2 = character2;
            IsFightFinished = false;
        }

        public Character Character1 { get => _character1; private set => _character1 = value; }
        public Character Character2 { get => _character2; private set => _character2 = value; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished { get; private set; }

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// La fonction varies avec les effet dont les pokémons sont affligés.
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            if (skillFromCharacter1 == null || skillFromCharacter2 == null)
            {
                throw new ArgumentNullException();
            }
            if (Character2.CurrentStatus is SleepStatus&& Character1.CurrentStatus is SleepStatus) { }
            else if (Character1.Speed >= Character2.Speed||Character2.CurrentStatus is SleepStatus)
            {
                if( Character1.CurrentStatus is CrazyStatus)
                {
                    Character1.Crazy();
                }
                else
                {
                    Character2.ReceiveAttack(skillFromCharacter1, Character1);
                }
                if (Character2.IsAlive&& Character2.CurrentStatus is not SleepStatus)
                {
                    if (Character2.CurrentStatus is CrazyStatus)
                    {
                        Character2.Crazy();
                    }
                    else
                    {
                        Character1.ReceiveAttack(skillFromCharacter2, Character2);
                    }
                }       
            }
            else
            {
                if (Character2.CurrentStatus is CrazyStatus)
                {
                    Character2.Crazy();
                }
                else
                {
                    Character1.ReceiveAttack(skillFromCharacter2, Character2);
                }
                if (Character1.IsAlive&& Character1.CurrentStatus is not SleepStatus)
                {
                    if (Character1.CurrentStatus is CrazyStatus)
                    {
                        Character1.Crazy();
                    }
                    else
                    {
                        Character2.ReceiveAttack(skillFromCharacter1, Character1);
                    }
                }
                
            } 
            if (Character1.CurrentStatus is BurnStatus)
            {
                Character1.Burn();
            }
            if(Character2.CurrentStatus is BurnStatus)
            {
                Character2.Burn();
            }
            if (Character1.CurrentStatus != null)
            {
                Character1.CurrentStatus.EndTurn();
            }
            if (Character2.CurrentStatus != null)
            {
                Character2.CurrentStatus.EndTurn();
            }
            if (!Character2.IsAlive || !Character1.IsAlive)
            {
                IsFightFinished = true;
            }


        }

    }
}
