﻿/*
 * Squidex Headless CMS
 *
 * @license
 * Copyright (c) Sebastian Stehle. All rights reserved
 */

import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {
    AnalyticsService,
    AutocompleteComponent,
    CanDeactivateGuard,
    ClipboardService,
    ConfirmClickDirective,
    ControlErrorsComponent,
    CopyDirective,
    DatePipe,
    DateTimeEditorComponent,
    DayOfWeekPipe,
    DayPipe,
    DialogService,
    DialogRendererComponent,
    DisplayNamePipe,
    DropdownComponent,
    DurationPipe,
    FileDropDirective,
    FileSizePipe,
    FocusOnInitDirective,
    FromNowPipe,
    ImageSourceDirective,
    IndeterminateValueDirective,
    JscriptEditorComponent,
    JsonEditorComponent,
    KeysPipe,
    KNumberPipe,
    LocalStoreService,
    LowerCaseInputDirective,
    MarkdownEditorComponent,
    MessageBus,
    ModalTargetDirective,
    ModalViewDirective,
    MoneyPipe,
    MonthPipe,
    OnboardingService,
    OnboardingTooltipComponent,
    PanelContainerDirective,
    PanelComponent,
    ParentLinkDirective,
    PopupLinkDirective,
    ProgressBarComponent,
    ResourceLoaderService,
    RootViewDirective,
    RootViewService,
    ScrollActiveDirective,
    ShortcutComponent,
    ShortcutService,
    ShortDatePipe,
    ShortTimePipe,
    SliderComponent,
    SortedDirective,
    StarsComponent,
    TagEditorComponent,
    TemplateWrapperDirective,
    TitleService,
    TitleComponent,
    ToggleComponent,
    UserReportComponent
} from './declarations';

@NgModule({
    imports: [
        HttpClientModule,
        FormsModule,
        CommonModule,
        RouterModule,
        ReactiveFormsModule
    ],
    declarations: [
        AutocompleteComponent,
        ConfirmClickDirective,
        ControlErrorsComponent,
        CopyDirective,
        DateTimeEditorComponent,
        DatePipe,
        DayOfWeekPipe,
        DayPipe,
        DialogRendererComponent,
        DisplayNamePipe,
        DropdownComponent,
        DurationPipe,
        FileDropDirective,
        FileSizePipe,
        FocusOnInitDirective,
        FromNowPipe,
        ImageSourceDirective,
        IndeterminateValueDirective,
        JscriptEditorComponent,
        JsonEditorComponent,
        KeysPipe,
        KNumberPipe,
        LowerCaseInputDirective,
        MarkdownEditorComponent,
        ModalTargetDirective,
        ModalViewDirective,
        MoneyPipe,
        MonthPipe,
        OnboardingTooltipComponent,
        PanelContainerDirective,
        PanelComponent,
        ParentLinkDirective,
        PopupLinkDirective,
        ProgressBarComponent,
        RootViewDirective,
        ScrollActiveDirective,
        ShortcutComponent,
        ShortDatePipe,
        ShortTimePipe,
        SliderComponent,
        SortedDirective,
        StarsComponent,
        TagEditorComponent,
        TemplateWrapperDirective,
        TitleComponent,
        ToggleComponent,
        UserReportComponent
    ],
    exports: [
        AutocompleteComponent,
        ConfirmClickDirective,
        ControlErrorsComponent,
        CopyDirective,
        DatePipe,
        DateTimeEditorComponent,
        DayOfWeekPipe,
        DayPipe,
        DialogRendererComponent,
        DisplayNamePipe,
        DropdownComponent,
        DurationPipe,
        FileDropDirective,
        FileSizePipe,
        FocusOnInitDirective,
        FromNowPipe,
        ImageSourceDirective,
        IndeterminateValueDirective,
        JscriptEditorComponent,
        JsonEditorComponent,
        KeysPipe,
        KNumberPipe,
        LowerCaseInputDirective,
        MarkdownEditorComponent,
        ModalTargetDirective,
        ModalViewDirective,
        MoneyPipe,
        MonthPipe,
        OnboardingTooltipComponent,
        PanelContainerDirective,
        PanelComponent,
        ParentLinkDirective,
        PopupLinkDirective,
        ProgressBarComponent,
        RootViewDirective,
        ScrollActiveDirective,
        ShortcutComponent,
        ShortDatePipe,
        ShortTimePipe,
        SliderComponent,
        SortedDirective,
        StarsComponent,
        TagEditorComponent,
        TemplateWrapperDirective,
        TitleComponent,
        ToggleComponent,
        UserReportComponent,
        HttpClientModule,
        FormsModule,
        CommonModule,
        RouterModule,
        ReactiveFormsModule
    ]
})
export class SqxFrameworkModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SqxFrameworkModule,
            providers: [
                AnalyticsService,
                CanDeactivateGuard,
                ClipboardService,
                DialogService,
                LocalStoreService,
                MessageBus,
                OnboardingService,
                ResourceLoaderService,
                RootViewService,
                ShortcutService,
                TitleService
            ]
        };
    }
 }