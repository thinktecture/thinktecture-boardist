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
import com.thinktecture.boardist.webApi.models.Category;

@Path("categories")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class CategoryEndpoint {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public List<Object> getAll() {
        return Category.findAll().stream().map(e -> ItemDto.fromItem((Category) e)).collect(Collectors.toList());
    }

    @GET
    @Path("{id}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getSingle(@PathParam("id") UUID id) {
        Category entity = Category.findById(id);
        if (entity == null) {
            return Response.status(404).build();
        }
        return Response.ok(ItemDto.fromItem(Category.findById(id))).build();
    }

    @DELETE
    @Path("{id}")
    @Transactional()
    public Response delete(@PathParam("id") UUID id) {
        Category delCategory = Category.findById(id);
        if (delCategory == null) {
            return Response.status(404).build();
        }
        if (delCategory.isPersistent()) {
            delCategory.delete();
        }
        return Response.ok().build();
    }

    @POST
    public Response post(Category data) {
        data.persistAndFlush();
        return Response.ok(data).build();
    }

    @PUT
    public void update(ItemDto data) {
        // preferred method would be BeanUtils.copyProperties
        Category old = Category.findById(data.id);
        old.copyOver(data);
        old.persistAndFlush();
    }

    @GET
    @Path("sync")
    public void sync() {
        // TODO sync
    }
}