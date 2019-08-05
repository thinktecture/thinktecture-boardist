package com.thinktecture.boardist.webApi.dtos;

import com.thinktecture.boardist.webApi.models.Item;
import com.thinktecture.boardist.webApi.models.Publisher;

public class PublisherItemDto extends ItemDto {

    public int priority;
    
    public void copyOver( Publisher other) {
        super.copyOver(other);
        this.priority = other.priority;
    }

    public static PublisherItemDto fromItem( Item other) {
        PublisherItemDto item =  (PublisherItemDto) ItemDto.fromItem(other);
        return item;
    }

}