local customCamZoom = true
local zoomValue = 0.8

function onCreate()
	if not customCamZoom then
        return
    end
end

function onUpdate()
    if not customCamZoom then
        return
    end

    setProperty('defaultCamZoom', zoomValue)
end

function onStepHit()
    if not customCamZoom then
        return
    end

	if curStep >= 0 and curStep <= 592 then
        zoomValue = mustHitSection and 0.8 or 1.2
    elseif curStep >= 592 and curStep <= 832 then
        zoomValue = 1.1
    elseif curStep >= 832 and curStep <= 1232 then
        zoomValue = mustHitSection and 0.8 or 1.2
    elseif curStep >= 1232 and curStep <= 1696 then
        zoomValue = mustHitSection and 1.1 or 1.2
    elseif curStep >= 1696 and curStep <= 2432 then
        zoomValue = mustHitSection and 1 or 0.9
    elseif curStep >= 2432 and curStep <= 2992 then
        zoomValue = mustHitSection and 0.8 or 1.1
    elseif curStep >= 2992 and curStep <= 4576 then
        zoomValue = mustHitSection and 1 or 0.6
    end
end