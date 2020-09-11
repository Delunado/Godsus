using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCombinationPossesion : StateBase {

    private int successfulButtonsNumber;
    private List<int> indexXbox = new List<int>();

    private string buttonsXbox = "ABXY";
    private string buttonsKeyboard = "WASD";

    float timer = 0f;
    float delay = 0.05f;
    bool canUseInput = true;

    private enum buttonsKeys
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3
    }

    private buttonsKeys keySelected;

    public StateCombinationPossesion(Player player) : base(player)
    { }

    public override void Tick()
    {
        if (timer >= delay)
        {
            canUseInput = true;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (canUseInput)
        {
            InputButtons();
        }

        if (successfulButtonsNumber == player.combinationButtonsNumber)
        {
            player.SetState(new StatePossesion(player));
            return;
        }

        if (player.selectedSer.Possesed)
        {
            player.SetState(new StateMovement(player));
            return;
        }
    }

    public override void OnStateEnter()
    {
        player.rb.velocity = Vector2.zero;
        successfulButtonsNumber = 0;
        GenerateCombination();
    }

    public override void OnStateExit()
    {
        player.textCombination.text = "";
    }

    private void GenerateCombination()
    {
        for (int i = 0; i < player.combinationButtonsNumber; i++)
        {
            int auxChar = UnityEngine.Random.Range(0, 4);
            indexXbox.Add(auxChar);

            player.textCombination.text += buttonsXbox[auxChar];
        }
    }

    private void InputButtons()
    {
        bool isInput = false;

        if (successfulButtonsNumber != player.combinationButtonsNumber)
        {
            if (player.input.ability1XIsDown)
            {
                isInput = true;
                keySelected = buttonsKeys.X;
            }
            else if (player.input.ability2AIsDown)
            {
                isInput = true;
                keySelected = buttonsKeys.A;
            }
            else if (player.input.ability3BIsDown)
            {
                isInput = true;
                keySelected = buttonsKeys.B;
            }
            else if (player.input.ability4YIsDown)
            {
                isInput = true;
                keySelected = buttonsKeys.Y;
            }

            if (isInput)
            {
                //Para reiniciar el timer
                canUseInput = false;
                timer = 0f;

                if ((int)keySelected == indexXbox[successfulButtonsNumber])
                {
                    successfulButtonsNumber++;
                    string newCombinationText = "";

                    for (int i = 0; i < player.combinationButtonsNumber; i++)
                    {
                        if (i < successfulButtonsNumber)
                            newCombinationText += "";
                        else
                            newCombinationText += buttonsXbox[indexXbox[i]];
                    }

                    player.textCombination.text = newCombinationText;
                }
                else
                {
                    player.SetState(new StateMovement(player));
                    return;
                }
            }
        }
    }

}
