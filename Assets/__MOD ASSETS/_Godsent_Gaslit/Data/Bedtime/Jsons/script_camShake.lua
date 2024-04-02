function opponentNoteHit()
    if getProperty('dad.curCharacter') == 'huggy' then
        cameraShake('camGame', 0.003, 0.1)
        cameraShake('camHUD', 0.001, 0.1)
    end
end