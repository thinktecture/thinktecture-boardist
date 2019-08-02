package com.thinktecture.boardist.webApi;

public enum FileCategoryEnum {

    RULES("rules"),
    LOGO("logo");

    public final String LABEL;

    private FileCategoryEnum(String label) {
        this.LABEL = label;
    }

    public String getCategoryValue() {
        return this.LABEL;
    }
}