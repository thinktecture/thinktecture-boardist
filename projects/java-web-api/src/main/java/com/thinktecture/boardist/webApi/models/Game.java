package com.thinktecture.boardist.webApi.models;

import java.sql.Date;
import java.util.List;
import java.util.UUID;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.Table;

import org.hibernate.annotations.GenericGenerator;

@Entity()
@Table(name = "Games", schema = "dbo", catalog = "boardist")
public class Game extends Item {

    @Id
    @GenericGenerator(name = "uuid", strategy = "uuid2")
    @GeneratedValue(generator = "uuid")
    @Column(columnDefinition = "UNIQUEIDENTIFIER")
    public UUID id;

    public String name;
    public Integer boardGameGeekId;
    public byte[] rowVersion;
    public Boolean isDeleted;

    public Integer maxPlayers;
    public Integer minPlayers;
    public Integer minAge;
    public Integer minDuration;
    public Integer maxDuration;
    public Float buyPrice;
    public Date buyDate;
    // public Boolean hasRules;

    // @OneToOne(optional = true)
    // @JoinColumn(name="Id")
    // public Publisher publisher;
    public UUID publisherId;


    public UUID mainGameId;

    @ManyToMany(cascade = {CascadeType.ALL}, fetch = FetchType.LAZY)
    @JoinTable(
        name = "GameMechanic", 
        joinColumns = {@JoinColumn(name = "GameId")},
        inverseJoinColumns= {@JoinColumn(name="MechanicId")})
    public List<Mechanic> mechanics;

    @ManyToMany(cascade = {CascadeType.ALL}, fetch = FetchType.LAZY)
    @JoinTable(
        name = "GameCategory", 
        joinColumns = {@JoinColumn(name = "GameId")},
        inverseJoinColumns= {@JoinColumn(name="CategoryId")})
    public List<Category> categories;

    @ManyToMany(cascade = {CascadeType.ALL}, fetch = FetchType.LAZY)
    @JoinTable(
        name = "GameAuthor", 
        joinColumns = {@JoinColumn(name = "GameId")},
        inverseJoinColumns= {@JoinColumn(name="AuthorId")})
    public List<Person> authors;

    @ManyToMany(cascade = {CascadeType.ALL}, fetch = FetchType.LAZY)
    @JoinTable(
        name = "GameIllustrator", 
        joinColumns = {@JoinColumn(name = "GameId")},
        inverseJoinColumns= {@JoinColumn(name="IllustratorId")})
    public List<Person> illustrators;

}
