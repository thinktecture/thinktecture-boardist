package com.thinktecture.boardist.webApi.dtos;

import java.util.UUID;

import com.thinktecture.boardist.webApi.models.Item;

public class ItemDto  {
    
    public UUID id;
    public String name;
    public int boardGameGeekId;

    public void copyOver( Item other) {
        this.name = other.name;
        this.boardGameGeekId = other.boardGameGeekId;
        this.id = other.id;
    }

    public static ItemDto fromItem( Item other) {
        ItemDto item = new ItemDto();
        item.name = other.name;
        item.boardGameGeekId = other.boardGameGeekId;
        item.id = other.id;
        return item;
    }
}
