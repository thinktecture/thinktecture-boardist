package com.thinktecture.boardist.webApi.dtos;

import java.sql.Date;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import com.thinktecture.boardist.webApi.models.Game;

public class GameDto {

    public UUID id;

    public String name;
    public Integer boardGameGeekId;

    public Integer maxPlayers;
    public Integer minPlayers;
    public Integer minAge;
    public Integer minDuration;
    public Integer maxDuration;
    public Float buyPrice;
    public Date buyDate;
    public Boolean hasRules;
    public UUID publisherId;
    public UUID mainGameId;
    public List<UUID> mechanics;
    public List<UUID> categories;
    public List<UUID> authors;
    public List<UUID> illustrators;


    public static GameDto fromGame(Game game) {
        GameDto dto = new GameDto();
        dto.name = game.name;
        dto.id = game.id;
        dto.hasRules = game.hasRules;
        dto.boardGameGeekId = game.boardGameGeekId;
        dto.maxPlayers = game.maxPlayers;
        dto.minPlayers = game.minPlayers;
        dto.minAge = game.minAge;
        dto.minDuration = game.minDuration;
        dto.maxDuration = game.maxDuration;
        dto.buyPrice = game.buyPrice;
        dto.buyDate = game.buyDate;
        dto.publisherId = game.publisherId;
        dto.mainGameId = game.mainGameId;
        dto.illustrators = game.illustrators.stream().map(e -> e.id).collect(Collectors.toList());;
        dto.authors = game.authors.stream().map(e -> e.id).collect(Collectors.toList());
        dto.categories = game.categories.stream().map(e -> e.id).collect(Collectors.toList());
        dto.mechanics = game.mechanics.stream().map(e -> e.id).collect(Collectors.toList());
        return dto;
    }

}
