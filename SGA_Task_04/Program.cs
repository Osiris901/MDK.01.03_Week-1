using System;
using System.Collections.Generic;
using System.Linq;

namespace SGA_Task_04
{
    internal class Program
    {
        private static int _kongenUses = 0;
        private static int _lastRiftUse = 0;
        private static int _spiritHealState = -1;

        private static int tick = 1;

        static void Main(string[] args)
        {
            var rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);
            var player = new Pawn("Игрок", rnd.Next(350, 650), rnd.NextDouble());
            var boss = new Pawn("Босс", rnd.Next(1000, 1500), rnd.NextDouble());

            var pawns = new List<Pawn>(3) {player, boss};
            string input = "";
            
            StartInfo(player, boss);

            while ((player.Health > 0 && boss.Health > 0) || input == "exit")
            {
                QuickInfo();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if (Estimate(pawns) == false) continue;
                        break;
                    case "2":
                        if (CastSpell(pawns) == false) continue;
                        break;
                    case "3":
                        Console.WriteLine("Вы пропускаете свой ход...");
                        break;
                    default:
                        Console.WriteLine("Команда не распознана, попробуйте ещё раз...");
                        continue;
                }


                if (boss.Health < 0) continue;
                // Boss logic
                Console.WriteLine("\nХод переходит боссу...");
                IEnumerable<Pawn> targets = pawns.Where(pawn =>
                    pawn.name != "Босс");
                /*
                Console.WriteLine("[DEBUG] Boss targets");
                foreach (var target in targets)
                {
                    Console.WriteLine(target);
                }*/

                var bossAtk = rnd.Next(0, 10);
                if (bossAtk == 6)
                {
                    Console.WriteLine("Раны босса быстро затягиваются. Он восстанавливает себе 200 хп.");
                    boss.Heal(200);
                } else if (bossAtk > 6)
                {
                    Console.WriteLine("Босс использует удар по области и наносит всем существам 50 урона");
                    foreach (var target in targets)
                    {
                        if (target.name == "Игрок" && _lastRiftUse == tick)
                        {
                            Console.WriteLine("Игрок находится в разломе и избегает получения урона.");
                            continue;
                        }
                        target.TakeDamage(50);
                    }
                }
                else
                {
                    var target = targets.ElementAt(rnd.Next(0, targets.Count()));
                    Console.WriteLine($"Босс атакует {target.name} и наносит 100 урона.");
                    if (target.name == "Игрок" && _lastRiftUse == tick)
                    {
                        Console.WriteLine("Игрок находится в разломе и избегает получения урона.");
                    }
                    else
                    {
                        target.TakeDamage(100);
                    }
                }

                _spiritHealState = Math.Max(-1, _spiritHealState--);
                tick++;
            }

            if (player.Health > 0)
            {
                Console.WriteLine("Вы победили!");
            }
            else
            {
                Console.WriteLine("Вам не удалось одолеть босса.");
            }

            Console.ReadKey();
        }

        private static bool Estimate(IEnumerable<Pawn> pawns)
        {
            Console.WriteLine("Выберите существо для оценки:");
            int c = 1;
            foreach (var pawn in pawns)
            {
                Console.WriteLine($"\t[{c}] {pawn.name}");
                c++;
            }
            Console.WriteLine($"\t[{c}] Отмена");

            string input = Console.ReadLine();

            if (Int32.TryParse(input, out int n))
            {
                if (n == c)
                    return false;

                var pawn = pawns.ElementAtOrDefault(n);
                if (pawn != null)
                {
                    Console.WriteLine(pawn);
                    return true;
                }
                Console.WriteLine("Не удалось оценить существо.");
                return true;
            }
            return false;
        }

        private static bool CastSpell(IList<Pawn> pawns)
        {
            var player = pawns.Single(pawn => pawn.name == "Игрок");
            var boss = pawns.Single(pawn => pawn.name == "Босс");
            var spirits = pawns.Where(pawn => pawn.name == "Теневой Дух");

            var rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second + 1);

            Console.WriteLine("Выберите заклинание:");

            Console.WriteLine($"\t[1] Конгён - магический шар, наносит 50 ед урона, увеличивает урон на 25 ед за каждое использование, отнимает 50 хп (Текущий урон - {_kongenUses * 25 + 50})");
            Console.WriteLine("\t[2] Рашамон - призывает теневого духа для нанесения атаки (Отнимает 100 хп игроку, максимум 3 духа)");
            Console.WriteLine("\t[3] Хуганзакура - каждый дух наносит до 100 ед урона (Может быть выполнен только после призыва теневого духа)");
            Console.WriteLine("\t[4] Поиджи сарам - случайный дух исчезает и накладывает на вас эффект лечения (50хп/ход, макс 250 хп)");
            Console.WriteLine("\t[5] Межпространственный разлом - позволяет скрыться в разломе (урон по вам не проходит) и восстановить до 250 хп (Откат 5 ходов)");
            Console.WriteLine("\t[6] Отмена");

            var input = Console.ReadLine();

            if (Int32.TryParse(input, out int n))
            {
                if (n == 6) return false;

                int damage = 0;
                switch (n)
                {
                    case 1:
                        damage = _kongenUses * 25 + 50;
                        Console.WriteLine(
                            $"Частицы магии собираются в шар и атакуют. Вы теряете 50 хп и наносите {damage} урона.");
                        player.TakeDamage(50);
                        boss.TakeDamage(damage);
                        break;
                    case 2:
                        if (spirits.Count() >= 3)
                        {
                            Console.WriteLine("Вы уже достигли максимальное число духов!");
                        }
                        else
                        {
                            var spirit = new Pawn("Теневой Дух", 100 + rnd.Next(25, 150), rnd.NextDouble());
                            pawns.Add(spirit);
                            player.TakeDamage(100);

                            Console.WriteLine($"Вы призываете нового фамильяра - {spirit}.");
                        }
                        break;
                    case 3:
                        damage = spirits.Sum(pawn => (int) (pawn.strength * 100));
                        Console.WriteLine($"Вы призыказываете своим духам атаковать и наносите {damage} урона.");
                        boss.TakeDamage(damage);
                        break;
                    case 4:
                        if (spirits.Any() && _spiritHealState == -1)
                        {
                            pawns.Remove(spirits.ElementAt(rnd.Next(0, spirits.Count())));

                            _spiritHealState += 5;
                            Console.WriteLine($"Вы жертвуете одним из своих духов и продлеваете эффект лечения на 5 ходов (Всего {_spiritHealState} ходов).");
                        }
                        else
                        {
                            Console.WriteLine("У вас нет призванных духов");
                        }
                        break;
                    case 5:
                        if (tick - _lastRiftUse > 4)
                        {
                            Console.WriteLine("Вы создаете небольшой разлом и прячетесь в нём, восстанавливая 250 хп.");
                            _lastRiftUse = tick;
                        }
                        else
                        {
                            Console.WriteLine("Вы пока не готовы создать разлом, подождите ещё " + (5 - (tick - _lastRiftUse)) + "ходов.");
                        }
                        break;
                    default:
                        Console.WriteLine("Неизвестное действие.");
                        return false;
                }
            }

            return false;
        }

        private static void QuickInfo()
        {
            Console.WriteLine($"Игровой такт № {tick}");
            Console.WriteLine("Вы начинаете свой ход, выберите действие:");
            Console.WriteLine("\t[1] Оценка - позволяет увидеть информацию о существе");
            Console.WriteLine("\t[2] Заклинание - выбор заклинания для применения");
            Console.WriteLine("\t[3] Пропуск хода.");
        }

        private static void StartInfo(Pawn player, Pawn boss)
        {
            Console.WriteLine("Битва с БОССОМ.");
            Console.WriteLine($"Вы - {player}, маг с несколькими заклинаниями в арсенале.");
            Console.WriteLine($"Враг - {boss}, древнее существо, способное наносить точные удары, удары по площади и исцелять свои раны.");
            Console.WriteLine("Бой начинается!");
        }
    }
}
