using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type

        [Test] 
        public void CharacterRecievedHeal()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var punch = new Punch();
            var oldHealth = pikachu.CurrentHealth;
            var gift = new Gift();

            pikachu.ReceiveAttack(punch);
            Assert.That(pikachu.CurrentHealth,
                Is.EqualTo(oldHealth - (punch.Power - pikachu.Defense)));
            pikachu.ReceiveAttack(gift);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(oldHealth));
        }
        [Test]
        public void CharacterRecievedNull()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var punch = new Punch();
            Assert.Throws<ArgumentNullException>(() =>
            {
                pikachu.ReceiveAttack(null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                pikachu.ReceiveAttack(punch,null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                pikachu.ReceiveAttack(null,pikachu);
            });

        }
        [Test]
        public void CharacterTakeStatus()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var fireBall = new FireBall();
            pikachu.ReceiveAttack(fireBall);
            Assert.That(pikachu.CurrentStatus.RemainingTurn, Is.EqualTo(StatusEffect.GetNewStatusEffect(fireBall.Status).RemainingTurn));
            Assert.That(pikachu.CurrentStatus.CanAttack, Is.EqualTo(StatusEffect.GetNewStatusEffect(fireBall.Status).CanAttack));
            Assert.That(pikachu.CurrentStatus.DamageEachTurn, Is.EqualTo(StatusEffect.GetNewStatusEffect(fireBall.Status).DamageEachTurn));
            Assert.That(pikachu.CurrentStatus.DamageOnAttack, Is.EqualTo(StatusEffect.GetNewStatusEffect(fireBall.Status).DamageOnAttack));
        }
        [Test]
        public void CharacterTakeBurnDamageAndStatusDecrement()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var pachirizu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var punch = new Punch();
            var fireBall = new FireBall();
            Fight f = new Fight(pikachu, pachirizu);
            f.ExecuteTurn(punch, fireBall);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(55));
            Assert.That(pikachu.CurrentStatus.RemainingTurn, Is.EqualTo(4));
        }
        [Test]
        public void CharacterTakeConfuseDamage()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var pachirizu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var punch = new Punch();
            var gift = new Gift();
            Fight f = new Fight(pikachu, pachirizu);
            f.ExecuteTurn(gift, punch);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(100));//le pokémon assaillant est confus donc pas de dégat
            Assert.That(pachirizu.CurrentHealth, Is.EqualTo(82));// mais il se tappe lui même
        }
        [Test]
        public void CharacterSleeping()
        {
            var pikachu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var pachirizu = new Character(100, 60, 30, 200, TYPE.NORMAL);
            var punch = new Punch();
            var grass = new MagicalGrass();
            Fight f = new Fight(pikachu, pachirizu);
            f.ExecuteTurn(grass, punch);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(100));// le pokémon assaillant est endormis donc pas de dégat
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CharacterLosingMaxHP()
        {
            var pikachu= new Character(100, 60, 30, 200, TYPE.NORMAL);
            var tabagisme = new Equipment(-30, 0, 0, 0);// fumer tue
            pikachu.Equip(tabagisme);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(70));
        }
        [Test]
        public void SpeedObject()
        {

            // Speed of pikachu is very high, so he attacks first and kills mewtwo

            Character pikachu = new Character(100, 10000, 30, 20, TYPE.NORMAL);
            Character mewtwo = new Character(1000, 500, 300, 200, TYPE.NORMAL);

            Fight f = new Fight(pikachu, mewtwo);

            var e = new Equipment(100, 90, 70, 1000);

            Punch p = new Punch();

            pikachu.Equip(e);

            f.ExecuteTurn(p, p);

            Assert.That(pikachu.IsAlive, Is.EqualTo(true));
            Assert.That(mewtwo.IsAlive, Is.EqualTo(false));
            Assert.That(f.IsFightFinished, Is.EqualTo(true));

            //pikachu lost his speed, so mewtwo attacks first, and kills pikachu
            pikachu.Unequip();

            Character mewtwo2 = new Character(1000, 500, 300, 200, TYPE.NORMAL);

            Fight f2 = new Fight(pikachu, mewtwo2);

            f2.ExecuteTurn(p, p);

            Assert.That(pikachu.IsAlive, Is.EqualTo(false));
            Assert.That(mewtwo2.IsAlive, Is.EqualTo(true));
            Assert.That(f.IsFightFinished, Is.EqualTo(true));
        }

        [Test]

        public void CalculateElement()
        {
            Character bulbizarre = new Character(1000, 50, 30, 20, TYPE.GRASS);
            Character charamander = new Character(1000, 50, 30, 20, TYPE.FIRE);
            Character waillord = new Character(1000, 50, 30, 20, TYPE.WATER);

            Fight f = new Fight(bulbizarre, charamander);
            Fight f2 = new Fight(bulbizarre, waillord);
            Fight f3 = new Fight(charamander, waillord);

            FireBall p = new FireBall();

            f.ExecuteTurn(p, p);
            f2.ExecuteTurn(p, p);
            f3.ExecuteTurn(p, p);

            Assert.That(bulbizarre.CurrentHealth, Is.LessThan(charamander.CurrentHealth));
            Assert.That(charamander.CurrentHealth, Is.LessThan(waillord.CurrentHealth));




        }




    }
}

