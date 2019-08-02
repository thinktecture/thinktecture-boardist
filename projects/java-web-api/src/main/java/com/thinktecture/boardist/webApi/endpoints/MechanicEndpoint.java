package com.thinktecture.boardist.webApi.endpoints;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import javax.transaction.Transactional;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import com.thinktecture.boardist.webApi.dtos.ItemDto;
import com.thinktecture.boardist.webApi.models.Mechanic;

@Path("mechanics")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class MechanicEndpoint {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public List<Object> getAll() {
        return Mechanic.findAll().stream().map(e -> ItemDto.fromItem((Mechanic) e)).collect(Collectors.toList());
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getSingle(@PathParam("id") UUID id) {
        Mechanic entity = Mechanic.findById(id);
        if (entity == null) {
            return Response.status(404).build();
        }
        return Response.ok(ItemDto.fromItem(Mechanic.findById(id))).build();
    }

    @DELETE
    @Path("{id}")
    @Transactional()
    public Response delete(@PathParam("id") UUID id) {
        Mechanic delMechanic = Mechanic.findById(id);
        if (delMechanic == null) {
            return Response.status(404).build();
        }
        if (delMechanic.isPersistent()) {
            delMechanic.delete();
        }
        return Response.ok().build();
    }

    @POST
    public Response post(Mechanic data) {
        data.persistAndFlush();
        return Response.ok(data).build();
    }

    @PUT
    public void update(ItemDto data) {
        // preferred method would be BeanUtils.copyProperties
        Mechanic old = Mechanic.findById(data.id);
        old.copyOver(data);
        old.persistAndFlush();
    }

    @GET
    @Path("sync")
    public void sync() {
        // TODO sync
    }
}