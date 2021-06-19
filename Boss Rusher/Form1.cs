using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

/** <=== BOSS RUSHER ===>
 * A game using the W, A and D keys to move about, the 1 (Gun) and 2 (Lance) number keys to switch weapons, and outlasting the boss long enough for it to die!
 * 
 * Over time by: 20 - 21 Hours
 * Created by: Ted Angus
 * Est. Hours spent: 16 - 18 hours  (Counting testing, creating sprites, the coding, sound creation)
 * Amount of different sounds: 8 (5 are for the boss alone)
 * Graphics created: 18 (5 are for the rank results, 5 for the boss, 4 for the player, 3 for the weapons, and 1 for the title)
 * Lines of code: More than 1,450
 * 
 * Changes from Designs:
 * - Boss has different color scheme and design [GAME]
 * - Bars do not have text or symbol beside them [GAME]
 * - Buttons are layered on-top of one another instead of side by side [MENU / WIN / LOSE]
 * - There is no text on the bottom [MENU]
 * - No crown above the player [WIN]
 * - Boss bar nor dead player shows on the lose screen, instead the boss stays, menacingly! [LOSE]
 * - Dialogue instead of title above [WIN / LOSE]
 * 
 * Changes from Flowchart:
 * - Buffed easy mode from 5 hp to 7
 * - Boss attacks are not 25% faster
 * - Likely more than 4 times a second for lance attack
 * - Phase 2 actually happens around 4000 HP instead of halfway (44%)
 * - On phase 1, boss always sweeps 3 times on attack 1 and 2
 * - There are only 2 attacks in phase 2 instead of 3
 * - Stomp was replaced with the fist attack
 * - Phase three was not developed at all, I was a little optimistic
 * - Usage of both weapons is not required for any of the ranks
 * - B rank is easier to obtain than before (50% of hp minimum instead of full hp required, 5 minutes instead of 3
 * - C rank is slightly easier to obtain than before (50% of hp minimum)
 * 
 * Tips for movement:
 * - You have a double jump, use them as needed
 * - You can slow your fall speed and increase jump height by holding W compared to just tapping it
 * - Movement is not slowed mid-air
 * 
 * Tips for Phase 1:
 * - The sweeping on ground attack occurs when the boss spins counter clockwise fastly
 * - The sweeping on all sides attack occurs when the boss spins clockwise fastly
 * - The bouncing across the screen attack occurs when the boss bounces in place twice
 * 
 * - The gun is very effective on the ground sweep attack, and slightly effective on the all sides attack
 * - For the bouncing attack, the lance is effective when it moves slow, but when it moves fast it's better to switch to the gun
 * - The lance is very effective in idle mode, since it does more damage per second than the gun, keep your distance around half health though
 * 
 * Bonus Tips for Cutscene:
 * - The boss still has collisions for all parts, keep your distance! It's still possible to hit it with the lance from a distance.
 * 
 * Tips for Phase 2:
 * - ONLY THE HEAD OF THE BOSS CAN BE DAMAGED!
 * - The boss will raise one hand if its going to summon an extra sawblade to fly around screen, these move at a moderate speed, can get annoying, but can easily be dodged
 * - The bosses' other attack (fist throwing) can be very difficult to dodge, I recommend continously jumping, wait for the right position! It throws each fist a total of three times.
 * - Since you can't get the same attack twice, you will always know what the next attack is on the second phase, since there's only two.
 * 
 * - It's difficult to do damage on the fist flying attack, just focus on dodging.
 * - The lance is very important on the second phase and will progress the fight much quicker, use it while it's idle or throwing the Sawblade since its not directly attacking you.
 * 
 * Other Tips:
 * - The Lance does more damage per second than the lance, use it when possible!
 * - The boss has 9,000 HP, with it transforming to the second phase around 4,000 HP
 * - Using your lance and gun at the right times, you only usually need to go through 3 - 4 first phase attacks, and for the second phase: 2 fist attacks and 2-3 saw blade attacks.
 * - Practice makes perfect!
 * 
 * 
 * The game doesn't seem to run at 50 FPS, I think this may actually be timer inconsistency instead of my game though, as I experience this with every program that has the timer involved!
 * If it does for you, then some of the sound variables may need adjusting, and the game may seem much more fast paced.
 * 
*/

namespace Boss_Rusher
{
    public partial class Form1 : Form
    {
        // Important
        Random randGen = new Random();
        long animation = 0;
        string scene = "menu"; // game, win, lose, menu

        // Sound Players
        System.Windows.Media.MediaPlayer lowSawSound = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer mediumSawSound = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer highSawSound = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer lowToHighSound = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer highToLowSound = new System.Windows.Media.MediaPlayer();
        int sawSoundMode = 0;
        int sawSoundCount = 0;
        int sawSoundUp = 0; //1 = up, 0 = neutral, -1 = down

        double swordSoundCooldown = 0;
        double theWalkPlayerInt = 0;

        // HP Bar stuff
        double playerHp = 5;
        double maxHp = 5;
        double timeTaken = 0;
        bool doesPlayerRegenerate = false;
        double playerHurtCooldown = 0;
        double bossHp = 9000; // 9000
        double bar1X = 0;
        
        // Weapon Variables
        bool curWeapon = true; // true is Gun, false is Lance
        int gunReload = 8; // 8 frames / bullet
        int lanceHitCooldown = 8;

        // Boss variables
        double bossPhase = 1;
        int bossAttackMode = 0; // 0 = Idle
        int bossAttackModeMode = 0; // double the mode, half the power
        int bossAttackExtraInt = 0; // for certain attacks
        double bossAttackAnotherExtraInt = 0; // another extra integer for 1 attack
        double bossExtraSpeed = 0;
        float bossRotation = 0;
        float bossRotationSpeed = 1;
        int phaseTime = 200;
        double bossX = 1100;
        double bossY = 575;
        double bossBodyY = 200;
        bool cutsceneFrontFloor = false;
        double bossHandSeperatorsX = 0;
        double hand1X = 0;
        double hand2X = 0;
        double hand1XSpeed = 0;
        double hand2XSpeed = 0;
        double raiseHandY = 0;
        double bossXSpeed = 0;
        double bossYSpeed = 0;
        int lastAttack = 0; // What was the bosses' last move? This is handy so the boss doesn't do the same attack right after.

        List<int> sawBladesX = new List<int>(new int[] { });
        List<int> sawBladesY = new List<int>(new int[] { });
        List<int> sawBladesDir = new List<int>(new int[] { });
        List<Rectangle> sawBladesCols = new List<Rectangle>(new Rectangle[] { });

        // Player movement
        double posX = 600;
        double posY = 575;
        double jumpsRemaining = 2;
        double posXSpeed = 0;
        double posYSpeed = 0;
        bool lastDirRight = true;
        double footFrame = 0;

        // Key pressings
        bool keyW = false;
        bool keyA = false;
        bool keyD = false;
        bool key1 = false;
        bool key2 = false;

        // Bullets and damage
        List<Rectangle> bulletsRight = new List<Rectangle>(new Rectangle[] {});
        List<Rectangle> bulletsLeft = new List<Rectangle>(new Rectangle[] {});
        int damageTakeDown = 0;
        List<int> damageTextX = new List<int>(new int[] { });
        List<int> damageTextY = new List<int>(new int[] { });
        List<int> damageTexts = new List<int>(new int[] { });
        List<int> damageTextTimers = new List<int>(new int[] { });

        // Brush
        SolidBrush theBrush = new SolidBrush(Color.DarkGray);

        // Collisions
        Rectangle stage = new Rectangle(0, 610, 1200, 60);
        Rectangle stage2 = new Rectangle(0, 635, 1200, 40);
        Rectangle stage3 = new Rectangle(0, 622, 1200, 60);

        Rectangle bossCol;
        Rectangle bossPhase2Col;
        Rectangle bossHand1Col;
        Rectangle bossHand2Col;
        Rectangle playerCol;
        Rectangle lanceCol;

        // Import images
        Image dRank = Properties.Resources.DRank; // All are 150 x 150
        Image cRank = Properties.Resources.CRank;
        Image bRank = Properties.Resources.BRank;
        Image aRank = Properties.Resources.ARank;
        Image sRank = Properties.Resources.SRank;

        Image playerBody = Properties.Resources.playerBody; // 24 x 37
        Image playerHead = Properties.Resources.playerHead; // 54 x 50
        Image playerHand = Properties.Resources.playerHand; // 13 x 13
        Image playerShoe = Properties.Resources.playerShoe; // 26 x 15
        Image lance = Properties.Resources.lance; // 200 x 60
        Image gun = Properties.Resources.gun; // 84 x 41
        Image bullet = Properties.Resources.bullet; // 16 x 2

        Image chainGear = Properties.Resources.chainGear; // Both are 100 x 100
        Image chainGearEyes = Properties.Resources.chainGearEyes;
        Image bossBody = Properties.Resources.bossBody; // 212 x 246
        Image bossHand = Properties.Resources.bossHand; // 92 x 89
        Image bossShoe = Properties.Resources.bossShoe; // 120 x 51

        // Setup sounds and 2 collisions
        public Form1()
        {
            InitializeComponent();
            playerCol = new Rectangle(Convert.ToInt32(Math.Floor(posX)) - 25, Convert.ToInt32(Math.Floor(posY)) - 25, 50, 70);
            bossCol = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) - 50, Convert.ToInt32(Math.Floor(bossY)) - 50, 100, 100);

            lowSawSound.Open(new Uri(Application.StartupPath + "/Resources/lowSaw.wav"));
            mediumSawSound.Open(new Uri(Application.StartupPath + "/Resources/mediumSaw.wav"));
            highSawSound.Open(new Uri(Application.StartupPath + "/Resources/highSaw.wav"));
            lowToHighSound.Open(new Uri(Application.StartupPath + "/Resources/lowToHighSaw.wav"));
            highToLowSound.Open(new Uri(Application.StartupPath + "/Resources/highToLowSaw.wav"));

        }

        // Sound methods
        private void playGunSound()
        {
            var theGunSound = new System.Windows.Media.MediaPlayer();

            theGunSound.Open(new Uri(Application.StartupPath + "/Resources/gunSound.wav"));

            theGunSound.Play();
        }
        private void playWalkSound()
        {
            var theWalkSound = new System.Windows.Media.MediaPlayer();

            theWalkSound.Open(new Uri(Application.StartupPath + "/Resources/walk.wav"));

            theWalkSound.Play();
        }
        private void playSwordSound()
        {
            var theSwordSound = new System.Windows.Media.MediaPlayer();

            theSwordSound.Open(new Uri(Application.StartupPath + "/Resources/sword.wav"));

            theSwordSound.Play();
        }

        // Game methods
        void resetGame()
        {
            // Reset most of the variables to allow the user try again
            animation = 0;

            timeTaken = 0;
            playerHurtCooldown = 0;
            bossHp = 9000;
            bar1X = 0;

            curWeapon = true;
            gunReload = 8;
            lanceHitCooldown = 8;

            bossPhase = 1;
            bossAttackMode = 0;
            bossAttackModeMode = 0;
            bossAttackExtraInt = 0;
            bossAttackAnotherExtraInt = 0;
            bossExtraSpeed = 0;
            bossRotation = 0;
            bossRotationSpeed = 1;
            phaseTime = 200;
            bossX = 1100;
            bossY = 575;
            bossBodyY = 200;
            cutsceneFrontFloor = false;
            bossHandSeperatorsX = 0;
            hand1X = 0;
            hand2X = 0;
            hand1XSpeed = 0;
            hand2XSpeed = 0;
            raiseHandY = 0;
            bossXSpeed = 0;
            bossYSpeed = 0;
            lastAttack = 0;

            sawBladesX.Clear();
            sawBladesY.Clear();
            sawBladesDir.Clear();
            sawBladesCols.Clear();

            posX = 600;
            posY = 575;
            jumpsRemaining = 2;
            posXSpeed = 0;
            posYSpeed = 0;
            lastDirRight = true;
            footFrame = 0;
            theWalkPlayerInt = 0;

            keyW = false;
            keyA = false;
            keyD = false;
            key1 = false;
            key2 = false;

            bulletsRight.Clear();
            bulletsLeft.Clear();
            damageTextX.Clear();
            damageTextY.Clear();
            damageTexts.Clear();
            damageTextTimers.Clear();
            damageTakeDown = 0;

            playerCol = new Rectangle(Convert.ToInt32(Math.Floor(posX)) - 25, Convert.ToInt32(Math.Floor(posY)) - 25, 50, 70);
            bossCol = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) - 50, Convert.ToInt32(Math.Floor(bossY)) - 50, 100, 100);

        }
        void bossPhases()
        {
            // Always update the phase
            phaseTime--;

            if (bossPhase == 1)
            { /**
               * Phase 1:
               * Three Attacks:
               * - Attack 1: Roll around the bottom of the screen three times
               * - Attack 2: Roll on all sides of the screen once 
               * - Attack 3: Bounce left to right multiple times
               * 
               * Idle time: 3 - 4 seconds
               * 
               */
                if (phaseTime < 0 && bossAttackMode == 0) // Idle
                {
                    bossAttackMode = randGen.Next(1, 4);
                    while (lastAttack == bossAttackMode)
                    {
                        bossAttackMode = randGen.Next(1, 4);
                    }
                    bossAttackModeMode = 0;
                    bossAttackAnotherExtraInt = 0;
                    if (bossAttackMode == 3)
                    {
                        bossYSpeed = -15;
                    }
                    else
                    {
                        sawSoundUp = 1;
                    }
                    phaseTime = 80;
                }
                if (bossAttackMode == 3) // Bounce
                {
                    if (bossAttackAnotherExtraInt < 2 && bossAttackModeMode == 0)
                    {
                        bossY += bossYSpeed;
                        bossYSpeed++;
                        if (bossY > 575)
                        {
                            bossY = 575;
                            bossYSpeed = -20;
                            bossAttackAnotherExtraInt++;
                        }
                    }
                    else if (bossAttackAnotherExtraInt >= 2 && bossAttackModeMode == 0)
                    {
                        bossAttackAnotherExtraInt = 0;
                        bossAttackExtraInt = 0;
                        bossAttackModeMode = 1;
                        phaseTime = 10000;
                        bossXSpeed = -7.5;
                    }
                    else if (bossAttackModeMode == 1)
                    {
                        bossX += bossXSpeed;
                        bossY += bossYSpeed;
                        bossYSpeed++;
                        if (bossY > 575)
                        {
                            bossY = 575;
                            bossYSpeed = -25;
                        }
                        if (bossX < 50)
                        {
                            bossX = 50;
                            bossXSpeed = -bossXSpeed * 1.2;
                            bossAttackExtraInt++;
                        }
                        if (bossX > 1150 && bossAttackExtraInt < 6)
                        {
                            bossX = 1150;
                            bossXSpeed = -bossXSpeed * 1.2;
                            bossAttackExtraInt++;
                        }
                        else if (bossX > 1150)
                        {
                            bossX = 1150;
                            bossXSpeed = -5;
                            bossAttackModeMode = 2;
                        }
                    }
                    else if (bossAttackModeMode == 2)
                    {
                        bossX += bossXSpeed;
                        bossY += bossYSpeed;
                        bossYSpeed++;
                        if (bossX < 1100)
                        {
                            bossX = 1100;
                            bossXSpeed = 0;
                        }
                        if (bossY > 575)
                        {
                            bossYSpeed = 0;
                            bossY = 575;
                        }

                        if (bossY == 575 && bossX == 1100)
                        {
                            bossXSpeed = 0;
                            bossYSpeed = 0;
                            bossAttackModeMode = 0;
                            bossAttackMode = 0;
                            phaseTime = randGen.Next(150, 200);
                            lastAttack = 3;
                        }
                    }
                }
                else if (bossAttackMode == 2) // Sweep on all sides
                {
                    if (phaseTime > 0 && bossAttackModeMode == 0)
                    {
                        if (bossRotationSpeed < 20)
                        {
                            bossRotationSpeed++;
                        }
                    }
                    else if (phaseTime <= 0 && bossAttackModeMode == 0)
                    {
                        bossAttackModeMode = 1;
                        phaseTime = 10000;
                        bossAttackExtraInt = 4;
                        bossAttackAnotherExtraInt = 0;
                        bossXSpeed = 0;
                        bossYSpeed = 0;
                        bossExtraSpeed = 0;
                    }
                    else if (bossAttackModeMode == 1)
                    {
                        bossY += bossYSpeed;
                        bossX += bossXSpeed;
                        if (bossAttackAnotherExtraInt <= 2)
                        {
                            if (bossAttackExtraInt == 4)
                            {
                                bossYSpeed -= 1 + bossExtraSpeed;
                                if (bossY < 100)
                                {
                                    bossY = 100;
                                    bossYSpeed = 0;
                                    bossAttackExtraInt = 3;
                                }
                            }
                            else if (bossAttackExtraInt == 3)
                            {
                                bossXSpeed -= 1 + bossExtraSpeed;
                                if (bossX < 100)
                                {
                                    bossX = 50;
                                    bossXSpeed = 0;
                                    bossAttackExtraInt = 2;
                                }
                            }
                            else if (bossAttackExtraInt == 2)
                            {
                                bossYSpeed += 1 + bossExtraSpeed;
                                if (bossY > 575)
                                {
                                    bossY = 575;
                                    bossYSpeed = 0;
                                    bossAttackExtraInt = 1;
                                }
                            }
                            else if (bossAttackExtraInt == 1)
                            {
                                bossXSpeed += 1 + bossExtraSpeed;
                                if (bossX > 1100)
                                {
                                    bossX = 1100;
                                    bossXSpeed = 0;
                                    bossYSpeed = 0;
                                    bossAttackExtraInt = 4;
                                    bossExtraSpeed += 0.5;
                                    bossAttackAnotherExtraInt++;
                                    if (bossAttackAnotherExtraInt == 3) { bossAttackExtraInt = 0; }
                                }
                            }
                        }
                        if (bossAttackExtraInt == 0)
                        {
                            bossXSpeed = 0;
                            bossYSpeed = 0;
                            bossAttackModeMode = 0;
                            bossAttackMode = 0;
                            phaseTime = randGen.Next(150, 200);
                            lastAttack = 2;
                            sawSoundUp = -1;
                        }
                    }

                }
                else if (bossAttackMode == 1) // Sweep the bottom of the screen
                {
                    if (phaseTime > 0 && bossAttackModeMode == 0) 
                    {
                        if (bossRotationSpeed > -20)
                        {
                            bossRotationSpeed--;
                        }
                    }
                    else if (phaseTime <= 0 && bossAttackModeMode == 0)
                    {
                        bossAttackModeMode = 1;
                        phaseTime = 10000;
                        bossAttackExtraInt = 6;
                        bossXSpeed = 0;
                        bossYSpeed = 0;
                        bossExtraSpeed = 0;
                    }
                    else if (bossAttackModeMode == 1 && bossAttackExtraInt > 0)
                    {
                        if (bossAttackExtraInt % 2 == 0)
                        {
                            bossXSpeed -= 1 + bossExtraSpeed;
                            if (bossX < 100)
                            {
                                bossX = 100;
                                bossXSpeed = 0;
                                bossAttackExtraInt--;
                                bossExtraSpeed += 0.5;
                            }
                        }
                        else
                        {
                            bossXSpeed += 1 + bossExtraSpeed;
                            if (bossX > 1100)
                            {
                                bossX = 1100;
                                bossXSpeed = 0;
                                bossAttackExtraInt--;
                                bossExtraSpeed += 0.5;
                            }
                        }
                        bossX += bossXSpeed;
                    }
                    else if (bossAttackModeMode == 1 && bossAttackExtraInt <= 0)
                    {
                        bossXSpeed = 0;
                        bossYSpeed = 0;
                        bossAttackModeMode = 0;
                        bossAttackMode = 0;
                        phaseTime = randGen.Next(150, 200);
                        lastAttack = 1;
                        sawSoundUp = -1;
                    }
                }
                else if (bossAttackMode == 0)
                { // Idle
                    if (bossHp < 4000)
                    {
                        bossPhase = 1.5;
                        bossAttackModeMode = 0;
                        bossAttackExtraInt = 60;
                        bossRotationSpeed = 5;
                        cutsceneFrontFloor = true;
                        sawSoundMode = 4;
                        sawSoundCount = 0;
                    }
                    if (bossRotationSpeed < 1)
                    {
                        bossRotationSpeed++;
                    }
                    else if (bossRotationSpeed > 1)
                    {
                        bossRotationSpeed--;
                    }
                }
            }
            else if (bossPhase == 1.5)
            { // Cutscene from phase 1 to phase 2
                if (bossAttackModeMode == 0)
                {
                    if (bossX > 1002)
                    {
                        bossX -= 5;
                    }
                    else
                    {
                        bossX = 1000 + Convert.ToDouble(randGen.Next(-2, 2));
                        bossAttackExtraInt--;
                    }
                    if (bossAttackExtraInt <= 0)
                    {
                        bossY--;
                        bossBodyY -= 3;
                        if (bossY < 500)
                        {
                            bossY = 500;
                            bossAttackModeMode = 1;
                        }
                    }
                }
                else if (bossAttackModeMode == 1)
                {
                    bossHandSeperatorsX += 3;
                    if (bossHandSeperatorsX > 150)
                    {
                        bossHandSeperatorsX = 150;
                        bossPhase = 2;
                        sawSoundMode = 5;
                        sawSoundCount = 0;
                        bossAttackExtraInt = 0;
                        bossAttackModeMode = 0;
                        phaseTime = 100;
                    }
                }
            }
            else if (bossPhase == 2)
            {/**
               * Phase 2:
               * Two Attacks:
               * - Attack 1: Spawn a saw-blade that goes around forever
               * - Attack 2: Throw both fists out periodically
               * 
               * Idle time: 2 - 3 seconds
               * 
               */
                if (phaseTime < 0 && bossAttackMode == 0) // Idle
                {
                    /*bossAttackMode = randGen.Next(1, 4);
                    while (lastAttack == bossAttackMode)
                    {
                        bossAttackMode = randGen.Next(1, 4);
                    }*/
                    if (lastAttack == 1)
                    {
                        bossAttackMode = 2;
                    }
                    else
                    {
                        bossAttackMode = 1;
                    }
                    bossAttackModeMode = 0;
                    bossAttackAnotherExtraInt = 0;
                    phaseTime = 80;
                    raiseHandY = 0;
                }
                else if (bossAttackMode == 2)
                { // Fist attack
                    if (bossRotationSpeed < 25)
                    {
                        bossRotationSpeed++;
                    }
                    else if (bossRotationSpeed > 25)
                    {
                        bossRotationSpeed--;
                    }
                    if (bossAttackModeMode == 0)
                    {
                        hand1X += 1;
                        hand2X += 1;
                        if (hand2X > 50)
                        {
                            hand2X = 50;
                            hand1X = 50;
                            hand1XSpeed = -40;
                            hand2XSpeed = 0;
                            phaseTime = 30;
                            bossAttackExtraInt = 0;
                            bossAttackModeMode = 1;
                        }
                    }
                    else if (bossAttackModeMode == 1)
                    {
                        hand1X += hand1XSpeed;
                        hand1XSpeed++;
                        if (phaseTime < 0)
                        {
                            hand2X += hand2XSpeed;
                            hand2XSpeed++;
                        }
                        if (bossAttackExtraInt < 6)
                        {
                            if (hand1X > 50)
                            {
                                hand1X = 50;
                                hand1XSpeed = -42;
                                bossAttackExtraInt++;
                            }
                            if (hand2X > 50)
                            {
                                hand2X = 50;
                                hand2XSpeed = -48;
                                bossAttackExtraInt++;
                            }
                        }
                        else
                        {
                            if (hand1X > 0 && hand1XSpeed > -1)
                            {
                                hand1X = 0;
                                hand1XSpeed = 0;
                            }
                            if (hand2X > 0 && hand2XSpeed > -1)
                            {
                                hand2X = 0;
                                hand2XSpeed = 0;
                            }
                            if (hand2X >= 0 && hand1X >= 0)
                            {
                                bossAttackModeMode = 0;
                                bossAttackMode = 0;
                                lastAttack = 2;
                                phaseTime = randGen.Next(80, 120);
                            }
                        }
                    }
                }
                else if (bossAttackMode == 1) // Permanent Sawblade
                {
                    if (bossRotationSpeed < -15)
                    {
                        bossRotationSpeed++;
                    }
                    else if (bossRotationSpeed > -15)
                    {
                        bossRotationSpeed--;
                    }
                    if (bossAttackModeMode == 0)
                    {
                        raiseHandY -= 2;
                        if (raiseHandY < -100)
                        {
                            raiseHandY = -100;
                            bossAttackModeMode = 1;
                            sawBladesDir.Add(0);
                            sawBladesCols.Add(new Rectangle(-50, -50, 0, 0));
                            sawBladesX.Add(Convert.ToInt32(Math.Floor(bossX)) - 40 + 50 + Convert.ToInt32(Math.Floor(Math.Sin((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(bossHandSeperatorsX)));
                            sawBladesY.Add(Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(raiseHandY)));
                            phaseTime = 120;
                        }
                    }
                    else if (bossAttackModeMode == 1)
                    {
                        sawBladesX[sawBladesDir.Count - 1] = Convert.ToInt32(Math.Floor(bossX)) - 40 + 50 + Convert.ToInt32(Math.Floor(Math.Sin((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(bossHandSeperatorsX));
                        sawBladesY[sawBladesDir.Count - 1] = Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(raiseHandY));
                        if (phaseTime < 0)
                        {
                            sawBladesDir[sawBladesDir.Count - 1] = 1;
                            bossAttackModeMode = 2;
                            phaseTime = 60;
                        }
                    }
                    else if (bossAttackModeMode == 2)
                    {
                        if (raiseHandY < 0)
                        {
                            raiseHandY += 2;
                        }
                        else
                        {
                            bossAttackModeMode = 0;
                            phaseTime = randGen.Next(80, 120);
                            bossAttackMode = 0;
                            lastAttack = 1;
                        }
                    }
                }
                else if (bossAttackMode == 0)
                { // Idle
                    if (bossRotationSpeed < 5)
                    {
                        bossRotationSpeed++;
                    }
                    else if (bossRotationSpeed > 5)
                    {
                        bossRotationSpeed--;
                    }
                }

            }
            bossRotation += bossRotationSpeed;

            // Reset the boss's rotation so it doesn't exceed the integer limit
            if (bossRotation > 360)
            {
                bossRotation -= 360;
            }
        }
        void bossSound()
        { /**
           * bossSound()
           * 
           * Used to add background sawblade noises (Recorded using automatic screwdriver)
           * Updates sound every 50 frames (not 1 second sadly due to regular visual studio lag)
           * 
           * */
            sawSoundCount--;

            // First phase sounds
            if (sawSoundCount < 0 && sawSoundMode == 0)
            {
                sawSoundCount = 50;
                lowSawSound.Stop();
                lowSawSound.Play();
            }
            if(sawSoundUp == 1 && sawSoundMode == 0)
            {
                sawSoundMode = 1;
                sawSoundCount = 50;
                lowSawSound.Stop();
                lowToHighSound.Play();
            }
            if(sawSoundMode == 1 && sawSoundCount < 0)
            {
                sawSoundMode = 2;
                sawSoundUp = 0;
                sawSoundCount = 50;
                lowToHighSound.Stop();
                highSawSound.Play();
            }
            else if (sawSoundCount < 0 && sawSoundMode == 2)
            {
                sawSoundCount = 50;
                highSawSound.Stop();
                highSawSound.Play();
            }
            if (sawSoundUp == -1 && sawSoundMode == 2)
            {
                sawSoundMode = 3;
                sawSoundCount = 50;
                highSawSound.Stop();
                highToLowSound.Play();
            }
            if (sawSoundMode == 3 && sawSoundCount < 0)
            {
                sawSoundMode = 0;
                sawSoundUp = 0;
                sawSoundCount = 50;
                highToLowSound.Stop();
                lowSawSound.Play();
            }

            // Second phase sounds
            if(sawSoundMode == 4 && sawSoundCount < 0)
            {
                sawSoundCount = 50;
                lowSawSound.Stop();
                highSawSound.Stop();
                lowToHighSound.Stop();
                highToLowSound.Stop();
                mediumSawSound.Stop();
                mediumSawSound.Play();
            }
            if (sawSoundMode == 5 && sawSoundCount < 0)
            {
                sawSoundCount = 50;
                mediumSawSound.Stop();
                mediumSawSound.Play();
                if(sawBladesDir.Count > 0)
                {
                    lowSawSound.Stop();
                    lowSawSound.Play();
                }
            }
        }
        void playerMovement()
        {
            /** playerMovement()
             * moves the player and jumps
             * A D to move left/right, W to jump (two jumps)
             * Also has walking sound
            */
            posX += posXSpeed;
            posY += posYSpeed;
            if (posX < 30)
            {
                posX = 30;
                posXSpeed = 0;
            }
            if (posX > this.Width - 30)
            {
                posX = this.Width - 30;
                posXSpeed = 0;
            }
            if (posY > 575)
            {
                posY = 575;
                posYSpeed = 0;
                jumpsRemaining = 2;
            }
            posYSpeed += 2;
            footFrame += posXSpeed * Math.PI;
            if (jumpsRemaining == 2) // Is the player on the ground?
            {
                theWalkPlayerInt += Math.Abs(posXSpeed * Math.PI);
            }
            if(theWalkPlayerInt >= 180)
            {
                theWalkPlayerInt -= 180;
                playWalkSound();
            }
            if (keyA)
            {
                posXSpeed -= 3;
                lastDirRight = false;
            }
            if (keyD)
            {
                posXSpeed += 3;
                lastDirRight = true;
            }
            if (keyW && jumpsRemaining > 0 && posYSpeed >= 0)
            {
                jumpsRemaining--;
                posYSpeed = -20;
            }

            if (keyW && jumpsRemaining < 2)
            {
                posYSpeed -= 1;
            }
            posXSpeed *= 0.8;

        }
        void gunAndLance()
        {
            /** gunAndLance()
             * Change the weapon you're holding
             * Do damage with the gun or lance
             * Shoot out bullets with the power of lists
             * Play bullet/sword sounds
             * 
            */

            // Switch weapons
            if (key1)
            {
                curWeapon = true;
            }
            else if (key2)
            {
                curWeapon = false;
            }

            // Gun/Lance cooldown
            gunReload--;
            if (gunReload < -100)
            {
                gunReload = -1;
            }
            lanceHitCooldown--;
            if (lanceHitCooldown < -100)
            {
                lanceHitCooldown = -1;
            }

            // Create bullets
            if (gunReload <= 0 && lastDirRight && curWeapon)
            {
                gunReload = 3;
                playGunSound();
                bulletsRight.Add(new Rectangle(Convert.ToInt32(Math.Floor(posX)) + 25 - 8, Convert.ToInt32(Math.Floor(posY)) + 3 - 1, 16, 2));
            }
            else if (gunReload <= 0 && curWeapon)
            {
                gunReload = 3;
                playGunSound();
                bulletsLeft.Add(new Rectangle(Convert.ToInt32(Math.Floor(posX)) - 25 - 8, Convert.ToInt32(Math.Floor(posY)) + 3 - 1, 16, 2));
            }

            // Delete bullets if they hit sides of screens
            for (int i = 0; i < bulletsRight.Count; i++)
            {
                if (bulletsRight[i].X > this.Width + 100)
                {
                    bulletsRight.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            for (int i = 0; i < bulletsLeft.Count; i++)
            {
                if (bulletsLeft[i].X < -100)
                {
                    bulletsLeft.RemoveAt(i);
                    i--;
                    continue;
                }
            }

            // Move bullets and check if they collide with the boss
            for (int i = 0; i < bulletsRight.Count; i++)
            {
                if (bulletsRight[i].IntersectsWith(bossCol))
                {
                    damageTakeDown = randGen.Next(12, 15);
                    bossHp -= damageTakeDown;
                    damageTextX.Add(randGen.Next(bulletsRight[i].X - 10 + 50, bulletsRight[i].X + 11 + 50));
                    damageTextY.Add(randGen.Next(bulletsRight[i].Y - 10, bulletsRight[i].Y + 11));
                    damageTexts.Add(damageTakeDown);
                    damageTextTimers.Add(40);
                    bulletsRight.RemoveAt(i);
                    i--;
                    continue;
                }
                bulletsRight[i] = new Rectangle(bulletsRight[i].X + 50, bulletsRight[i].Y, 16, 2);
            }
            for (int i = 0; i < bulletsLeft.Count; i++)
            {
                if (bulletsLeft[i].IntersectsWith(bossCol))
                {
                    damageTakeDown = randGen.Next(12, 15);
                    bossHp -= damageTakeDown;
                    damageTextX.Add(randGen.Next(bulletsLeft[i].X - 10 - 50, bulletsLeft[i].X + 11 - 50));
                    damageTextY.Add(randGen.Next(bulletsLeft[i].Y - 10, bulletsLeft[i].Y + 11));
                    damageTexts.Add(damageTakeDown);
                    damageTextTimers.Add(40);
                    bulletsLeft.RemoveAt(i);
                    i--;
                    continue;
                }
                bulletsLeft[i] = new Rectangle(bulletsLeft[i].X - 50, bulletsLeft[i].Y, 16, 2);
            }

            // Lance hitting code, first statement is right facing while other is left
            if (lastDirRight && !curWeapon)
            {
                swordSoundCooldown--;
                if(swordSoundCooldown < 0)
                {
                    swordSoundCooldown = 20;
                    //playSwordSound();
                }
                lanceCol = new Rectangle(Convert.ToInt32(Math.Floor(posX)) + 24 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4)), Convert.ToInt32(Math.Floor(posY)) - 18, 200, 60);
                if (lanceCol.IntersectsWith(bossCol) && lanceHitCooldown < 0)
                {
                    damageTakeDown = randGen.Next(23, 27);
                    bossHp -= damageTakeDown;
                    int longNumber = Convert.ToInt32(Math.Floor(posX)) + 24 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4));
                    damageTextX.Add(randGen.Next(longNumber + 100, longNumber + 200));
                    damageTextY.Add(randGen.Next(Convert.ToInt32(Math.Floor(posY)) - 18, Convert.ToInt32(Math.Floor(posY)) + 18));
                    damageTexts.Add(damageTakeDown);
                    damageTextTimers.Add(40);
                    lanceHitCooldown = 4;
                }
            }
            else if (!curWeapon)
            {
                swordSoundCooldown--;
                if (swordSoundCooldown < 0)
                {
                    swordSoundCooldown = 20;
                    //playSwordSound();
                }
                lanceCol = new Rectangle(Convert.ToInt32(Math.Floor(posX)) - 24 - 200 - Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4)), Convert.ToInt32(Math.Floor(posY)) - 18, 200, 60);
                if (lanceCol.IntersectsWith(bossCol) && lanceHitCooldown < 0)
                {
                    damageTakeDown = randGen.Next(23, 27);
                    bossHp -= damageTakeDown;
                    int longNumber = Convert.ToInt32(Math.Floor(posX)) + 24 - 200 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4));
                    damageTextX.Add(randGen.Next(longNumber, longNumber + 100));
                    damageTextY.Add(randGen.Next(Convert.ToInt32(Math.Floor(posY)) - 18, Convert.ToInt32(Math.Floor(posY)) + 18));
                    damageTexts.Add(damageTakeDown);
                    damageTextTimers.Add(40);
                    lanceHitCooldown = 4;
                }
            }
        }
        void updateDamageText()
        {
            // Remove damage text if enough time has passed
            for (int i = 0; i < damageTexts.Count; i++)
            {
                damageTextTimers[i]--;
                if (damageTextTimers[i] < 0)
                {
                    damageTextTimers.RemoveAt(i);
                    damageTexts.RemoveAt(i);
                    damageTextX.RemoveAt(i);
                    damageTextY.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }
        void updateSawBlades()
        {
            // Move the sawblades in the specified direction, and change direction if they pass a boundary
            for (int i = 0; i < sawBladesDir.Count; i++)
            {
                sawBladesCols[i] = new Rectangle(sawBladesX[i] - 25, sawBladesY[i] - 25, 50, 50);
                if (sawBladesCols[i].IntersectsWith(playerCol) && playerHurtCooldown < 0)
                {
                    playerHp--;
                    playerHurtCooldown = 100;
                }
                if (sawBladesDir[i] == 1)
                {
                    sawBladesY[i] -= 7;
                    if (sawBladesY[i] < 25)
                    {
                        sawBladesY[i] = 25;
                        sawBladesDir[i] = 2;
                    }
                }
                else if (sawBladesDir[i] == 2)
                {
                    sawBladesX[i] -= 7;
                    if (sawBladesX[i] < 25)
                    {
                        sawBladesX[i] = 25;
                        sawBladesDir[i] = 3;
                    }
                }
                else if (sawBladesDir[i] == 3)
                {
                    sawBladesY[i] += 7;
                    if (sawBladesY[i] > 600)
                    {
                        sawBladesY[i] = 600;
                        sawBladesDir[i] = 4;
                    }
                }
                else if (sawBladesDir[i] == 4)
                {
                    sawBladesX[i] += 7;
                    if (sawBladesX[i] > 1175)
                    {
                        sawBladesX[i] = 1175;
                        sawBladesDir[i] = 1;
                    }
                }
            }
        }
        void updateCollisions()
        {
            // Update boss related collisions
            bossCol = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) - 50, Convert.ToInt32(Math.Floor(bossY)) - 50, 100, 100);
            bossHand1Col = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) + Convert.ToInt32(Math.Floor(hand1X)) + 70 - 92 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 10) * 4)) - Convert.ToInt32(Math.Floor(bossHandSeperatorsX)), Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((animation / 57.2958) * 10) * 10)), 92, 89);
            bossHand2Col = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) + Convert.ToInt32(Math.Floor(hand2X)) - 40 + 50 + Convert.ToInt32(Math.Floor(Math.Sin((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(bossHandSeperatorsX)), Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((-animation / 57.2958) * 10) * 10)) + Convert.ToInt32(Math.Floor(raiseHandY)), 92, 89);
            bossPhase2Col = new Rectangle(Convert.ToInt32(Math.Floor(bossX)) - 106 + 50, Convert.ToInt32(Math.Floor(bossY)) - 123 + Convert.ToInt32(Math.Floor(bossBodyY)), 212, 246);
            playerCol = new Rectangle(Convert.ToInt32(Math.Floor(posX)) - 25, Convert.ToInt32(Math.Floor(posY)) - 25, 50, 70);

            // If the mode is easy, regenerate 1 hp every 30 seconds
            if (doesPlayerRegenerate && playerHp < maxHp)
            {
                playerHp += 0.0007;
            }

            // If the player intersects with the boss collisions then lose 1 hp
            if (playerCol.IntersectsWith(bossCol) && playerHurtCooldown < 0)
            {
                playerHp--;
                playerHurtCooldown = 100;
            }
            if (bossPhase >= 1.5 && (playerCol.IntersectsWith(bossPhase2Col) || playerCol.IntersectsWith(bossHand1Col) || playerCol.IntersectsWith(bossHand2Col)) && playerHurtCooldown < 0)
            {
                playerHp--;
                playerHurtCooldown = 100;
            }
        }

        private void gameTick_Tick(object sender, EventArgs e)
        {
            if (scene == "game")
            { 
                // Update timeTaken and cooldowns
                timeTaken+=0.02;
                if (playerHurtCooldown > -5)
                {
                    playerHurtCooldown--;
                }

                // Methods
                bossPhases();
                bossSound();
                animation++;
                playerMovement();
                gunAndLance();
                updateDamageText();
                updateSawBlades();
                updateCollisions();
                
                // If the player is dead, send them to the lose screen and show necessary objects
                if(playerHp <= 0)
                {
                    scene = "lose";
                    dialogueLabel1.Text = "You died!";
                    dialogueLabel1.Visible = true;
                    dialogueLabel2.Visible = true;
                    menuButton.Visible = true;
                    exitButton.Visible = true;
                    dialogueLabel2.Text = "Feel free to try a different difficulty or exit";
                }

                // If the boss is dead, the player wins
                if(bossHp <= 0)
                {
                    scene = "win";
                    dialogueLabel1.Text = "Great work on beating the only boss!";
                    dialogueLabel1.Visible = true;
                    dialogueLabel2.Text = "Your rank is...";
                    phaseTime = 100;
                }
            }
            else if(scene == "win")
            { 
                // Show the first label, then the second a second later, and the rank two seconds after the first label.
                phaseTime--;
                if(phaseTime < 50)
                {
                    dialogueLabel2.Visible = true;
                }
                if (phaseTime < 0)
                {
                    phaseTime = -5;
                    /** LABEL SYSTEM
                     * S-Rank: Must be on Hard mode, took less than 3 minutes to beat, and full hp
                     * A-Rank: Took less than 3 minutes to beat, and full hp
                     * B-Rank: Took less than 5 minutes to beat, and more than half hp
                     * C-Rank: Took less than 5 minutes to beat, or more than half hp
                     * D-Rank: If the player took 5 minutes or more to beat, and had less than half hp
                    */
                    if(timeTaken < 180 && playerHp == maxHp && maxHp == 3)
                    {
                        rankBox.Image = sRank;
                    }
                    else if(timeTaken < 180 && playerHp >= maxHp)
                    {
                        rankBox.Image = aRank;
                    }
                    else if (timeTaken < 300 && playerHp >= maxHp / 2)
                    {
                        rankBox.Image = bRank;
                    }
                    else if (timeTaken < 300 || playerHp >= maxHp / 2)
                    {
                        rankBox.Image = cRank;
                    }
                    else
                    {
                        rankBox.Image = dRank;
                    }
                    rankBox.Visible = true;
                    menuButton.Visible = true;
                    exitButton.Visible = true;
                }
            }
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Backdrop
            theBrush.Color = Color.FromArgb(50, 50, 50);
            e.Graphics.FillRectangle(theBrush, stage);
            theBrush.Color = Color.FromArgb(40, 40, 40);
            e.Graphics.FillRectangle(theBrush, stage2);

            if (scene == "game" || scene == "win")
            {
                // Player
                if (playerHurtCooldown < 1 || (playerHurtCooldown % 8 < 4))
                {
                    e.Graphics.DrawImage(playerBody, Convert.ToInt32(Math.Floor(posX)) - 12, Convert.ToInt32(Math.Floor(posY)));
                    if (lastDirRight)
                    {
                        e.Graphics.DrawImage(playerHead, Convert.ToInt32(Math.Floor(posX)) - 30, Convert.ToInt32(Math.Floor(posY)) - 40);
                    }
                    else
                    {
                        e.Graphics.DrawImage(playerHead, Convert.ToInt32(Math.Floor(posX)) + 30, Convert.ToInt32(Math.Floor(posY)) - 40, -54, 50);
                    }
                    e.Graphics.DrawImage(playerHand, Convert.ToInt32(Math.Floor(posX)) + 25 - 6, Convert.ToInt32(Math.Floor(posY)) + 5);
                    e.Graphics.DrawImage(playerHand, Convert.ToInt32(Math.Floor(posX)) - 25 - 6, Convert.ToInt32(Math.Floor(posY)) + 6);
                    e.Graphics.DrawImage(playerShoe, Convert.ToInt32(Math.Floor(posX)) + 13 - 13 + Convert.ToInt32(Math.Floor(Math.Sin(footFrame / 57.2958) * 10)), Convert.ToInt32(Math.Floor(posY)) + 35);
                    e.Graphics.DrawImage(playerShoe, Convert.ToInt32(Math.Floor(posX)) - 13 - 13 - Convert.ToInt32(Math.Floor(Math.Sin(footFrame / 57.2958) * 10)), Convert.ToInt32(Math.Floor(posY)) + 35);
                }


                for (int i = 0; i < bulletsRight.Count; i++)
                {
                    e.Graphics.DrawImage(bullet, bulletsRight[i].X, bulletsRight[i].Y);
                }
                for (int i = 0; i < bulletsLeft.Count; i++)
                {
                    e.Graphics.DrawImage(bullet, bulletsLeft[i].X, bulletsLeft[i].Y);
                }


                if (playerHurtCooldown < 1 || (playerHurtCooldown % 8 < 4))
                {
                    // Draw Weaponary as needed
                    if (lastDirRight && curWeapon)
                    {
                        e.Graphics.DrawImage(gun, Convert.ToInt32(Math.Floor(posX)) - 30 - 6, Convert.ToInt32(Math.Floor(posY)));
                    }
                    else if (curWeapon)
                    {
                        e.Graphics.DrawImage(gun, Convert.ToInt32(Math.Floor(posX)) + 30 + 6, Convert.ToInt32(Math.Floor(posY)), -84, 41);
                    }


                    if (lastDirRight && !curWeapon)
                    {
                        e.Graphics.DrawImage(lance, Convert.ToInt32(Math.Floor(posX)) + 24 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4)), Convert.ToInt32(Math.Floor(posY)) - 18);
                    }
                    else if (!curWeapon)
                    {
                        e.Graphics.DrawImage(lance, Convert.ToInt32(Math.Floor(posX)) - 24 - Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 25) * 4)), Convert.ToInt32(Math.Floor(posY)) - 18, -200, 60);
                    }
                }
            }
            if (scene == "game" || scene == "lose")
            {

                // Phase 2 parts
                if (bossPhase > 1)
                {
                    e.Graphics.DrawImage(bossShoe, Convert.ToInt32(Math.Floor(bossX)) - 60 + 212 - 90, Convert.ToInt32(Math.Floor(bossY)) - 25 + 130 + Convert.ToInt32(Math.Floor(bossBodyY)));
                    e.Graphics.DrawImage(bossShoe, Convert.ToInt32(Math.Floor(bossX)) - 60 + 90, Convert.ToInt32(Math.Floor(bossY)) - 25 + 130 + Convert.ToInt32(Math.Floor(bossBodyY)), -120, 51);
                    if (bossAttackModeMode == 1 || bossPhase > 1.5)
                    {
                        e.Graphics.DrawImage(bossHand, Convert.ToInt32(Math.Floor(bossX)) + Convert.ToInt32(Math.Floor(hand1X)) + 70 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 10) * 4)) - Convert.ToInt32(Math.Floor(bossHandSeperatorsX)), Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((animation / 57.2958) * 10) * 10)), -92, 89);
                        e.Graphics.DrawImage(bossHand, Convert.ToInt32(Math.Floor(bossX)) + Convert.ToInt32(Math.Floor(hand2X)) - 40 + 50 + Convert.ToInt32(Math.Floor(Math.Sin((-animation / 57.2958) * 10) * 4)) + Convert.ToInt32(Math.Floor(bossHandSeperatorsX)), Convert.ToInt32(Math.Floor(bossY)) - 40 + 10 + Convert.ToInt32(Math.Floor(Math.Cos((-animation / 57.2958) * 10) * 10)) + Convert.ToInt32(Math.Floor(raiseHandY)));
                    }
                    e.Graphics.DrawImage(bossBody, Convert.ToInt32(Math.Floor(bossX)) - 106 + 50, Convert.ToInt32(Math.Floor(bossY)) - 123 + Convert.ToInt32(Math.Floor(bossBodyY)));
                }

                // To hide the body from appearing in front of the floor during the cutscene
                if (cutsceneFrontFloor)
                {

                    theBrush.Color = Color.FromArgb(50, 50, 50);
                    e.Graphics.FillRectangle(theBrush, stage3);
                    theBrush.Color = Color.FromArgb(40, 40, 40);
                    e.Graphics.FillRectangle(theBrush, stage2);
                }


                for (int i = 0; i < sawBladesDir.Count; i++)
                {
                    e.Graphics.TranslateTransform(sawBladesX[i], sawBladesY[i]);
                    e.Graphics.RotateTransform(animation * 8);
                    e.Graphics.DrawImage(chainGear, -25, -25, 50, 50);
                    e.Graphics.ResetTransform();
                }

                // Phase 1 part
                e.Graphics.TranslateTransform(Convert.ToInt32(Math.Floor(bossX)), Convert.ToInt32(Math.Floor(bossY)));
                e.Graphics.RotateTransform(bossRotation);
                e.Graphics.DrawImage(chainGear, -50, -50);
                e.Graphics.ResetTransform();
                e.Graphics.DrawImage(chainGearEyes, Convert.ToInt32(Math.Floor(bossX)) - 50, Convert.ToInt32(Math.Floor(bossY)) - 50 + Convert.ToInt32(Math.Floor(Math.Sin((animation / 57.2958) * 2) * 4)));


                theBrush.Color = Color.FromArgb(200, 100, 25);
                for (int i = 0; i < damageTexts.Count; i++)
                {
                    e.Graphics.DrawString(damageTexts[i].ToString(), DefaultFont, theBrush, damageTextX[i] - 10, damageTextY[i] - 10);
                }

                
            }
            if (scene == "game")
            {
                // Boss Bar
                bar1X = this.Width - 50 - (500 * (bossHp / 9000));
                theBrush.Color = Color.FromArgb(100, 25, 25);
                e.Graphics.FillRectangle(theBrush, this.Width - 555, 15, 510, 40);
                theBrush.Color = Color.FromArgb(200, 50, 50);
                e.Graphics.FillRectangle(theBrush, Convert.ToInt32(Math.Floor(bar1X)), 20, Convert.ToInt32(Math.Floor(500 * (bossHp / 9000))), 30);


                // Player Bar
                theBrush.Color = Color.FromArgb(25, 100, 25);
                e.Graphics.FillRectangle(theBrush, 45, 15, 210, 40);
                theBrush.Color = Color.FromArgb(50, 200, 50);
                e.Graphics.FillRectangle(theBrush, 50, 20, Convert.ToInt32(Math.Floor(200 * (playerHp / maxHp))), 30);
                /*e.Graphics.FillRectangle(theBrush, playerCol);
                theBrush.Color = Color.FromArgb(225, 100, 25);
                e.Graphics.FillRectangle(theBrush, bossCol);
                e.Graphics.FillRectangle(theBrush, bossPhase2Col);
                e.Graphics.FillRectangle(theBrush, bossHand1Col);
                e.Graphics.FillRectangle(theBrush, bossHand2Col);*/
            }
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            // Hide labels and buttons and reset the game, allow regeneration and 7hp
            if(scene == "menu")
            {
                scene = "game";
                doesPlayerRegenerate = true;
                playerHp = 7;
                maxHp = 7;
                easyButton.Visible = false;
                normalButton.Visible = false;
                hardButton.Visible = false;
                exitButton.Visible = false;
                titleBox.Visible = false;
                resetGame();
                this.Focus();
            }
        }

        private void normalButton_Click(object sender, EventArgs e)
        {
            // Hide labels and buttons and reset the game, don't allow regeneration and 5hp
            if (scene == "menu")
            {
                scene = "game";
                doesPlayerRegenerate = false;
                playerHp = 5;
                maxHp = 5;
                easyButton.Visible = false;
                normalButton.Visible = false;
                hardButton.Visible = false;
                exitButton.Visible = false;
                titleBox.Visible = false;
                resetGame();
                this.Focus();
            }

        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            // Hide labels and buttons and reset the game, don't allow regeneration and 3hp
            if (scene == "menu")
            {
                scene = "game";
                doesPlayerRegenerate = false;
                playerHp = 3;
                maxHp = 3;
                easyButton.Visible = false;
                normalButton.Visible = false;
                hardButton.Visible = false;
                exitButton.Visible = false;
                titleBox.Visible = false;
                resetGame();
                this.Focus();
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Update key variables when pressed down
            switch (e.KeyCode)
            {
                case Keys.W:
                    keyW = true;
                    break;
                case Keys.A:
                    keyA = true;
                    break;
                case Keys.D:
                    keyD = true;
                    break;
                case Keys.D1:
                    key1 = true;
                    break;
                case Keys.D2:
                    key2 = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Update key variables when said key is released
            switch (e.KeyCode)
            {
                case Keys.W:
                    keyW = false;
                    break;
                case Keys.A:
                    keyA = false;
                    break;
                case Keys.D:
                    keyD = false;
                    break;
                case Keys.D1:
                    key1 = false;
                    break;
                case Keys.D2:
                    key2 = false;
                    break;
            }

        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            // Show all the menu buttons and hide win/lose labels
            scene = "menu";
            menuButton.Visible = false;
            easyButton.Visible = true;
            normalButton.Visible = true;
            hardButton.Visible = true;
            dialogueLabel1.Visible = false;
            dialogueLabel2.Visible = false;
            rankBox.Visible = false;
            titleBox.Visible = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Leave the game
            Application.Exit();
        }
    }
}
