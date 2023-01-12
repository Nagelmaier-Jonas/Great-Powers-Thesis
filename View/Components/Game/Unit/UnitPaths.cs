﻿using Model.Entities.Units;
using Model.Entities.Units.Abstract;
using Model.Factories;

namespace View.Components.Game.Unit;

public static class UnitPaths{
    public static List<AUnit> Units{ get; set; } = new List<AUnit>(){
        LandUnitFactory.CreateInfantry(null, null),
        LandUnitFactory.CreateTank(null, null),
        LandUnitFactory.CreateAntiAir(null, null),
        LandUnitFactory.CreateArtillery(null, null),
        PlaneFactory.CreateBomber(null, null),
        PlaneFactory.CreateFighter(null, null),
        ShipFactory.CreateCruiser(null, null),
        ShipFactory.CreateDestroyer(null, null),
        ShipFactory.CreateSubmarine(null, null),
        ShipFactory.CreateTransport(null, null),
        ShipFactory.CreateBattleship(null, null),
        ShipFactory.CreateAircraftCarrier(null, null),
        IndustryFactory.Create(null)
    };

    public static Dictionary<AUnit, string> Paths = new Dictionary<AUnit, string>(){
        {
            Units[0],
            "M48.32,22.45c.03-.39,.03-.91,.03-.91-.3-.27-.42-.85-.42-.85-.78-.63-1.21-1.69-1.21-1.69,.09-.48,.63-.72,.63-.72,.48-.82,1.15-1.06,1.15-1.06,0,0,1.48-3.83,2.99-4.44,1.3-.85,3.53-.21,3.53-.21,3.2,.91,3.35,3.08,3.35,3.08,.06,.45-.3,3.92-.3,3.92,.63,.24,.66,1.54,.66,1.54,.3,1.6-2.05,1.6-3.17,1.72-.39,1.75-1.57,2.51-1.57,2.51v1.21c1.24,.82,2.6,2.23,2.6,2.23,.66,1,.45,3.74,.45,3.74l-.42,.94c.51,.57-.21,3.08-.21,3.08,0,1-.48,1.48-.48,1.48v1.69c.09,.54-.51,1.27-.51,1.27,.24,.45,.45,1.84,.45,1.84l.48,.94,2.63-.15c0-.72,1-.06,1-.06l13.92-.33v-.45c-.06-.51,.51-.48,.51-.48,0,0,.91-.12,.91,.48,0,.15-.06,.36-.06,.36h1c.3,.69,.15,1.69,.15,1.69l-3.41,.21-.06,.21-12.86,.94c-.39,.54-1.06,.06-1.06,.06l-2.2,.21c-.21,.33-.69,.72-.69,.72,.09,1.33-1.21,1.21-1.21,1.21-.85,.27-2.26,.15-2.26,.15-.6-.24-1.42-.94-1.42-.94-.63,.78,.27,.78-.85,1.9-.18,.36,.15,1.48,.15,1.48,.12,.42-.78,.24-.78,.24,.54,1.36,2.17,3.53,2.26,3.89,.24,.66,.42,2.23,.42,2.23,0,0,1.54,2.11,1.54,2.99,.18,.48-.69,2.23-.69,2.23l-.94,2.54-2.69,7.28c-.24,.57-.06,1.78-.06,1.78,0,0-.15,.91,.06,1.27,.3,.39,.42,1.06,.42,1.06,.33,.3,1.66,.63,1.66,.63,.36,.15,.97,.72,.97,.72,1.78,.18,.78,1.9,.78,1.9l.15,.94-6.79,.06c-.63-.21-1.42-.72-1.42-.72l-1.84,.06s-1.42-.63-1.42-.94c.03-.33,.36-1.36,.36-1.36v-1.18c.18-.66,.63-1.66,.63-1.66,0,0,.12-.82,.21-1.12,.12-.27,.63-.78,.63-.78,.48-1.24,.42-4.74,2.57-10.02-.94-1,.42-1.78,.42-1.78,.72-1.03,0-1.42,0-1.42-1.33-.85-2.32-1.81-2.42-2.2-.18,.54-1.54,1.93-1.54,1.93l-1.9,3.62c-.45,.63-1.54,1.9-1.54,1.9,.15,.33-1.48,2.2-1.48,2.2l-1.78,2.96-1.84,2.63c.12,.6-.51,2.02-.51,2.02,.21,.24-.03,1.36,.24,1.87,.33,.12,1.75,1.21,1.75,1.21,1.78,.06,.85,1.84,.85,1.84l-.09,.94-4.8-.06c-1.3-.36-2.2-1.57-2.2-1.57-.27-1.39-.82-2.11-.82-2.11l-1.21-.42c-.69-.72-1.27-1.99-1.27-1.99l1-1.06c-.09-.82,.97-1.54,.97-1.54,.36-.18-.06-1.03,1.27-1.42,1.66-4.8,4.56-8.42,4.56-8.42-.75-.78,.21-2.69,.21-2.69l1.27-3.53c.15-2.9,1.72-3.96,1.72-3.96-.75-.66-1.3-2.26-1.3-2.26-.48-.39-.94-1.06-.94-1.06l-4.86,2.54c-1.24,.45-1.21-.57-1.21-.57l.21-3.23c.06-.57,.54-.48,.54-.48l5.52-.72,.51-.42-.15-.51c-.91,.72-1.87-.33-1.87-.33l-1.6-4.59-.78-2.08c-1.06-1.48-.94-3.68-.94-3.68,.09-.69,1.54-1.69,1.54-1.69,.51-.54,.48-1.33,.66-1.63,.3-.21,1.48-.51,1.48-.51,.48-.12,.69-1.15,.91-1.42,.82-.63,1.18-.36,1.48-.69,.48-.3,.72-1.06,.72-1.06,.42-.33,1.09-.06,1.27-.63,.6-1.45,3.56-2.75,3.56-2.75l3.71-.24,1.78-1.42,.72-.06h-.09Zm-9.84,14.7l-.48-.94v-.48c-.39,.15-.82,.78-.82,.78l.12,.69,1.18-.09v.03Z"
        },{
            Units[1],
            "M83.2,52.18c.49-.08,.74-1.19,.74-1.19-.56-1.07-2.24-2.42-2.24-2.42-.88-.5-2.49-.76-2.49-.76l-6.73,.13-.06-2.24h-4.48l-.06-4.35,1.68-.5,.93-.49h12.39c.06,.37,.45,.55,.45,.55h3.23c.57,0,.69-.74,.69-.74l-.06-3.18s-.26-.74-.88-.8c-1.05-.06-2.67,0-2.67,0-.37,0-.68,.43-.68,.43l-17.5-.06c-1.31-2.12-4.42-2.12-4.42-2.12h-8.4l.06-1.5h-2.75l-.12,1.38-3.23,.06-.43-.76h-3.49l-.49,.88h-.45l-3.23,7.84-8.83,.06-2.06-.76h-4.37l-4.36,3.24-1.81-.62c-.8-.37-1.6-.06-1.6-.06l-3.06,2.05c-.74,.56-.37,1.36-.37,1.36l.31,1.13-3.41,1.62c-.2,.8,.25,1.42,.25,1.42-1.5,1.44-1.44,3.92-1.44,3.92,1.05,5.36,7.72,5.92,7.72,5.92l29.25,.43,29.5-.06c2.49-.25,5.79-2.92,5.79-2.92,2.3-2.05,3.24-4.6,3.24-4.6,.13-.57-.06-2.3-.06-2.3"
        },{
            Units[2],
            "M17.35,75.56h60.3v-3.08l-19.66-.42v-.31l-2.75-1.62h-3.84s-1.12-1.92-1.12-2.54v-7.38h2.06s.94,.53,2.06,.53v-.73h2.65v-1.95h1.59l1.75-2.93h-5.74s.11-.37-.53-1.06c-.64-.69-1.58-1.66-1.58-1.66,0,0,.31-2.22-.75-4.09l1.98-2.73-.72-.69,6.46-8.57-1.37-1.11,10.13-13.94-2.46-1.84-19.04,24.77-.5-.33-2.78,3.62,.45,.47-3.5,4.54-.53-.37-2.78,3.57,.5,.48-.72,.86,2.28,1.83-2.04,2.82,2.95,2.2,2.71-3.53s.8,.51,1.72,.2v4.17l-4.57,4.01h-2.71v3.01h-.36v.28l-19.54,.51v3Z"
        },{
            Units[3],
            "M11.98,58.68v-.83c0-.05,3.19-.05,3.19-.05l.46-.29s-.28-.63-.28-.68,5.07-2.2,5.07-2.2l.26-3.38,.55-.13s.02-.81,0-.86c-.02-.05,1.84-.79,1.84-.79l.2,.66,.84-.28,1.54,3.58,1.21-.55v-.5l1.94-.88,.15-.03c0-.07-.02-.13-.02-.19,0-1.05,.85-1.91,1.91-1.91,.89,0,1.64,.62,1.84,1.44l15.81-2.65-.22-1.6,.83-.13-.18-.82,.84-.15-1.14-6.11,3.27-.62,.02,.62,1.67-.26-.07-.68,3.14-.55,.15,.79,3.84-.59-.84-4.96,3.82-.68,.79,4.88,4.88-.82-.13-.57,1.63-.27,.11,.55,12.39-2.11,.7,3.76-8.46,1.56,.66,3.82-10.33,1.84,1.55,8.8-3.03,.56c.03,.25,.04,.5,.04,.76,0,4.04-3.27,7.32-7.32,7.32-3.74,0-6.81-2.8-7.26-6.42l-20,3.84s-.6-3.84-.65-3.86c-.06-.02-8.04,3.41-8.04,3.41l-.6,3.08H11.87l-.85-3.89h.96Z"
        },{
            Units[4],
            "M87.7,40.31c-.06-.89-.47-2.23-3.01-2.58-1.25-.17-9.6-1.25-17.76-2.3,0-.02,.01-.03,.01-.04-.01-.16-.18-.33-.42-.42v-2.34c.21-.09,.36-.31,.36-.55v-.75c0-.33-.27-.6-.6-.6h-2.56c-.33,0-.6,.27-.6,.6v.75c0,.24,.15,.46,.36,.55v1.91c-.23,.04-.41,.19-.46,.39-1.59-.2-3.12-.4-4.54-.58,0-.02,.01-.03,.01-.05-.01-.16-.18-.33-.42-.42v-2.34c.21-.09,.36-.3,.36-.55v-.75c0-.33-.27-.6-.6-.6h-2.56c-.33,0-.6,.27-.6,.6v.75c0,.25,.15,.46,.36,.55v1.91c-.23,.04-.41,.2-.47,.4-2.07-.27-3.55-.46-4.09-.53v-4.53c0-5.1-1.34-9.24-2.99-9.24s-2.99,4.14-2.99,9.24v4.53c-.53,.07-2.01,.26-4.08,.53-.05-.2-.24-.35-.47-.4v-1.91c.21-.09,.36-.3,.36-.55v-.75c0-.33-.27-.6-.6-.6h-2.56c-.33,0-.6,.27-.6,.6v.75c0,.25,.15,.46,.36,.55v2.34c-.23,.09-.41,.26-.42,.42,0,.02,.01,.03,.01,.05-1.42,.18-2.95,.38-4.54,.58-.05-.2-.24-.35-.46-.39v-1.91c.21-.09,.36-.31,.36-.55v-.75c0-.33-.27-.6-.6-.6h-2.56c-.33,0-.6,.27-.6,.6v.75c0,.24,.15,.46,.36,.55v2.34c-.24,.09-.41,.26-.42,.42,0,.02,0,.03,.01,.04-8.16,1.05-16.51,2.13-17.76,2.3-2.55,.34-2.95,1.69-3.01,2.58-.08,1.12,.41,2.2,2.13,2.55,1.68,.34,33.83,3.23,35.32,3.37l.14,2.05-.41,.12,.23,2.92h.39l.87,12.93c-6.01,1.38-11.81,2.68-12.73,2.8-2.01,.27-2.33,1.34-2.38,2.04-.06,.89,.33,1.74,1.69,2.01,.66,.13,7.28,.29,13.92,.42l.27,3.94h1.55l.26-3.94c6.63-.13,13.26-.29,13.92-.42,1.36-.27,1.75-1.13,1.69-2.01-.05-.7-.37-1.77-2.38-2.04-.92-.12-6.72-1.43-12.73-2.8l.87-12.93h.39l.23-2.92-.41-.12,.14-2.05c1.49-.13,33.64-3.03,35.32-3.37,1.72-.34,2.21-1.42,2.13-2.55"
        },{
            Units[5],
            "M47.5,16.92c1.38,.11,1.27,1.28,1.27,1.28h1.3c2.36,1.24,1.79,6.81,1.79,6.81-.02,.35-.36,.4-.36,.4l-.16,.98,29.34,3.09c3.4,.32,4.78,4.68,4.69,5.64-.44,5.5-4.79,5.94-4.79,5.94l-29.77,3.85-.7,18.48,11.43,5.13c2.2,1.09,1.12,3.95-.42,4.25-.74,.06-11.26,.65-11.26,.65v3.19c0,1.55-1.09,1.49-1.09,1.49h-2.54s-1.09,.06-1.08-1.49v-3.19s-10.53-.59-11.27-.65c-1.54-.29-2.62-3.16-.42-4.25l11.43-5.13-.7-18.48-29.77-3.84s-4.36-.44-4.8-5.94c-.1-.96,1.29-5.32,4.69-5.64l29.34-3.09-.16-.98s-.34-.05-.36-.4c0,0-.57-5.57,1.79-6.81h1.3s-.11-1.18,1.27-1.28h0Z"
        },{
            Units[6],
            "M89.04,55.35c1.05-1.58,1.74-3.75,1.74-3.75,.03-1.06-.92-1.06-.92-1.06l-9.86,.17v-.89h-3.74v-.52h-3.36v-.69h-3.51v-.52l-2.96,.03-.03-.5-1.08,.03v-2.04h-.69v-1.25l-2.07-.56-.03-1.54-.76,.03,.03-1.35h-.29v-1.32h-1.81v1.38h-.33l.06,2.99-.36,.06v.56l-.46,.03,.03,.92h-2.07l-.23-1.78h-3.19l.56,5.33-8.83,.03-.03-.63h-1.91l.03-2.5-1.05,.03-.1-1.68-2.86,.07,.1,2.33h-.52v-.65h-2.57v1.41h-.92l-.03-.63-1.68-.03-.03,3.12-1.15,.1v-1.45h-1.02l.06-.79-5.65-.07c0-.52-.63-.62-.63-.62l-1.08-.03c-.69,.06-.76,.72-.76,.72l.03,.79h-1.8l-.06,1.34-1.84-.03,.03,.55h-3.02v.47h-3.39l.04,1.08-8.87,.03,.1,3.13,84.73,.1Z"
        },{
            Units[7],
            "M75.8,56.64H9.36s-.25-.34-.25-1.74h2.22v-1.65h2.55l.42-.54h.61v.17h.89v-.4l1.45,.37,.61-.02v-1.19h2.55l.44-.53h.61v.19h.4v.3h.86v.38h.8v.79h.28v.51h.89v-.24h.24l.35-.4h5.3v-.66h.56v-.65h1.37v-.21h.87v.84h.84v.67h4.46l.67,.37h1.29v-1.54h.14v-.77h1.1v.75h.21v.96h1.64l-.4-2.66,.23-.09,.05-.4,2.22-.33,.32,.37h.21l.14,.63h.98l.28-5.34-.77-.52v-.91h2.57v.89l-.77,.52v3.48h1.23v-.51h.19l.14-.14h.37l.15,.15h.46v.46l1.65,.16v.6h1.19l.16,.39h.4l.09,.09h.56l1.08,.38v.94h1.65v-.43h.88v-.28h.47v-.19h.56l.46,.45h2.59v1.04l.94-.25h.36v.36h.72v-.21h.56l.54,.55h2.52v.95l.52-.11v.15l3.11-.26v.16l2.38-.12v.37l2.92-.15-1.86,4.05Z"
        },{
            Units[8],
            "M5.37,53.62s-1.57-1.3-1.66-1.79c0-1,1.08-1.13,1.08-1.13l8.09-.03v-.59h.39l.06-.33h.96l.03-.22,26.97,.08,.06-.36h2.96l.03-.97h4.15l.03,.92,2.6,.06v.33l.63,.03,.03-1.79h-.25l.03-.72-.22-.03,.05-.63h3.79l-.03-2.87h1.19v-2.46h2.15l.06,2.38,1.13-.03-.03,2.74,3.61,.03v.22h1.64l.03,2.98,10.5-.19v-.44l1.3-.03,.03,.33,9.56-.37,.03-.47,1.16-.03v.47l2.46-.03c1.41,0,1.33,1.53,1.33,1.53-.33,4.22-1.88,3.67-1.88,3.67"
        },{
            Units[9],
            "M9.15,49.6c.15,0,.27-.12,.27-.27v-.3c0-.15,.12-.27,.26-.27h.51c.15,0,.27,.13,.27,.27v.26c0,.15,.09,.27,.21,.26,.12,0,.22,.12,.22,.26v.54c0,.15,.13,.27,.27,.27h9.36c.15,0,.27,.04,.27,.08,0,.04,.12,.08,.27,.08h.04c.15,0,.27-.05,.27-.1s.12-.09,.27-.09h.51c.15,0,.27,.06,.27,.13s.12,.13,.27,.13h.79c.15,0,.27-.07,.27-.14,0-.08,.12-.14,.27-.14h.16c.14,0,.27-.12,.27-.27v-1.33c0-.15,.08-.27,.17-.27s.17-.12,.17-.27v-7.94c0-.15,.12-.27,.27-.27h1.65c.15,0,.27,.12,.27,.27v7.84c0,.15,.06,.27,.13,.27s.13,.12,.13,.27v1.43c0,.15,.11,.27,.25,.27s.25,.07,.25,.16,.12,.16,.27,.16h3.51c.15,0,.27-.12,.27-.27v-1.33c0-.15,.12-.27,.26-.27h3.83c.15,0,.27-.12,.27-.27v-.38c0-.15,.12-.27,.27-.27h.22c.15,0,.27,.12,.27,.27v.35c0,.15,.12,.27,.27,.27h1.02c.14,0,.27-.12,.27-.27v-1.77c0-.15,.12-.27,.26-.27h.1c.15,0,.27,.12,.27,.27v.86c0,.15,.09,.27,.2,.27s.2,.12,.2,.27v.42c0,.15,.12,.27,.27,.27h.79c.14,0,.26-.12,.26-.27v-.13c0-.15,.12-.27,.27-.27h1.71c.15,0,.27-.12,.27-.27v-2.41c0-.15,.12-.27,.27-.27h.32c.15,0,.27,.11,.27,.24s.12,.24,.27,.24h1.49c.15,0,.27-.12,.27-.27v-.99c0-.14,.04-.27,.09-.27s.1-.12,.1-.27v-1.36c0-.15,.12-.27,.27-.27h.92c.15,0,.27,.12,.27,.27v1.33c0,.15,.07,.27,.16,.27s.16,.12,.16,.27v1.02c0,.15,.12,.27,.27,.27h1.74c.14,0,.27-.12,.27-.27h0c0-.15,.12-.27,.27-.27h.35c.15,0,.27,.12,.27,.27v1.08c0,.15,.12,.27,.27,.27h.07c.14,0,.27,.12,.27,.27v3.32c0,.15,.12,.27,.27,.27h.1c.15,0,.27-.04,.27-.08,0-.04,.12-.08,.27-.08h5.41c.15,0,.27-.12,.27-.27v-1.65c0-.15,.06-.27,.13-.27s.13-.12,.13-.27v-8.38c0-.15,.12-.27,.27-.27h1.62c.15,0,.27,.12,.27,.27v8.35c0,.15,.06,.27,.14,.27s.14,.12,.14,.27v1.65c0,.15,.12,.27,.27,.27h9.93c.15,0,.27-.12,.27-.27v-1.39c0-.15,.07-.27,.16-.27s.16-.12,.16-.27v-8.41c0-.15,.12-.27,.27-.27h1.55c.15,0,.27,.12,.27,.27v8.44c0,.15,.05,.27,.11,.27,.06,0,.11,.12,.11,.27v1.33c0,.15,.12,.27,.27,.27h3.42c.15,0,.3-.04,.35-.08,.04-.04,.15-.08,.24-.08s.2,.03,.24,.08c.04,.04,.2,.08,.34,.08h4.68c.15,0,.27-.12,.27-.27v-.79c0-.15,.12-.27,.27-.27h1.17c.15,0,.27,.12,.27,.27v.04c0,.15,.12,.27,.27,.27h1.68c.15,0,.27,.11,.27,.25s.12,.25,.27,.25h1.99c.15,0,.27,.12,.29,.27,0,0,.24,3.53-1.67,4.81H6.92c-.15,0-.36-.08-.47-.17l-1.59-3.12c-.07-.13-.13-.35-.13-.5v-.95c0,.06,.05,.06,.12,0,.06-.07,.12-.24,.12-.39v-.43c0-.15,.12-.27,.27-.27h3.91Z"
        },{
            Units[10],
            "M87.02,52.54l2.02-3.17c.44-1.02-.62-1.09-.94-1.06-3.18,.41-11.78,1.17-11.78,1.17v-.36l-5.13-.26-.36-.64h-.81v-.35l-5.13-.18-.58-.67h-2.62l-1.5,.61v-.71h-3.14l-.3,.1v-.45l-.14-.03v-.68c0-.38-.58-.45-.58-.45h-2.28s-.48,0-.55,.38v.75h-.51v-.68l-.14-.03v-1.84l-.62-.04-.13-2.18-4.33,.03,.03,1.63,1.09-.03-.1,4.06c-2.36-1.13-2.29-1.63-2.29-1.63-.17-.55-.17-1.57-.17-1.57l-.3-.3-.03-.65c.14-.89-.79-.54-.79-.54l-2.38,.51c-.45,.17-.38,.62-.38,.62h-.14l-.1,5.19h-2.49v-1.95l-3.07,.03v-1.43c-.03-.52-.51-.65-.51-.65l-1.3,.03s-.54,.06-.54,.51v1.47l-1.71,.03c.03-.58-.47-.58-.47-.58h-1.64s-.4,.1-.37,.58c-.27-.03-.55,0-.55,0v-.37l-3.07-.03-.07,.68-1.08-.41-2.73,.04-.47,.61-5.08,.21,.03,.47h-.31l-.58,.59-5.01,.1-.03,.71-9.14-.07c.48,2.32,4.33,3.07,4.33,3.07l80.98-.17Z"
        },{
            Units[11],
            "M5.94,53.82c-.22-1.4-1.51-2.92-1.51-2.92v-1.19l1.3-.11,.22-1.73,32.64,.22v-1.4h2.48v-.65h.87v-1.4l4.32,.11v-3.57h4.22v3.57h1.08v1.3h1.3l-.11,.87h2.7l.11,1.3,33.72-.11v1.19l1.3,.11-.11,1.51c-1.62,.54-1.84,2.81-1.84,2.81l-82.68,.11Z"
        },{
            Units[12],
            "M19.8,71.1h55.4V29.5h-5.5v-5.5h-8.3v5.5v13.9L47.5,29.5v13.9L33.6,29.5v13.9L19.8,29.5V71.1"
        }
    };
}