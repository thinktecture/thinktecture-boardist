package com.thinktecture.boardist.webApi.endpoints;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import javax.transaction.Transactional;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.HEAD;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import com.thinktecture.boardist.webApi.dtos.GameDto;
import com.thinktecture.boardist.webApi.models.Game;

import net.bytebuddy.asm.Advice.Return;

@Path("games")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class GamesEndpoint {

    @POST
    @Path("{id}/import")
    public Response importGame(@PathParam("id") UUID id) {
        // TODO handle param overwrite bool
        // TODO return true/false for import
        return Response.ok().build();
    }

    @GET
    @Path("lookup")
    public Response lookupGame(String query) {
        // TODO importer lookup
        return Response.ok().build();
    }

    @HEAD
    @Path("{id")
    public Response hasRules(UUID id) {
        Game game = Game.findById(id);
        if (game == null) {
            return Response.status(404).build();
        } else if (game.hasRules) {
            return Response.ok(true).build();
        } else {
            return Response.noContent().build();
        }
    }

    @GET
    public List<Object> getAll() {
        return Game.findAll().stream().map(e -> GameDto.fromGame((Game) e)).collect(Collectors.toList());
    }

    @GET
    @Path("{id}")
    public Response getSingle(@PathParam("id") UUID id) {
        Game entity = Game.findById(id);
        if (entity == null) {
            return Response.status(404).build();
        }
        return Response.ok(GameDto.fromGame(Game.findById(id))).build();
    }

    @DELETE
    @Path("{id}")
    @Transactional()
    public Response delete(@PathParam("id") UUID id) {
        Game delGame = Game.findById(id);
        if (delGame == null) {
            return Response.status(404).build();
        }
        if (delGame.isPersistent()) {
            delGame.delete();
        }
        return Response.ok().build();
    }

    @POST
    public Response post(Game data) {
        data.persistAndFlush();
        return Response.ok(data).build();
    }

    @PUT
    public void update(GameDto data) {
        // preferred method would be BeanUtils.copyProperties
        // Game old = Game.findById(data.id);
        // old.copyOver(data);
        // old.persistAndFlush();
    }

    @GET
    @Path("sync")
    public void sync() {
        // TODO sync
    }
}