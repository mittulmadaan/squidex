import { browser, by, element } from 'protractor';

import { ContentPage } from './ContentPage.po';

export class SearchPage extends ContentPage {
    public $contentsItem() {
        return element(by.xpath('//sqx-content-value/span'));
    }

    public $contentItems() {
        return element.all(by.xpath('//sqx-content-value/span'));
    }

    public $boldText() {
        return element(by.xpath('//div[@class=\'tui-editor-contents\']/div/b'));
    }

    public $italicText() {
        return element(by.xpath('//div[@class=\'tui-editor-contents\']/div/i'));
    }

    public $bulletPointText() {
        return element(by.xpath('//div[@class=\'tui-editor-contents\']/div/ul/li'));
    }

    public $numberedText() {
        return element(by.xpath('//div[@class=\'tui-editor-contents\']/div/ol/li'));
    }

    public $commentaryInput() {
        return element(by.xpath('//div[@contenteditable=\'true\']/div'));
    }

    public async getCommentaryCreationSuccessMessageText() {
        return await this.waitForElementToBeVisibleAndGetText(element(by.xpath('//div[@class=\'alert alert-dismissible alert-info ng-trigger ng-trigger-fade ng-star-inserted\']/span')));
    }

    public async getCommentaryCreationFailureMessageText() {
        return await this.waitForElementToBeVisibleAndGetText(element(by.xpath('//div[@class=\'alert alert-dismissible alert-danger ng-trigger ng-trigger-fade ng-star-inserted\']/span')));
    }

    public async getRefDataSelection(referenceName: string) {
        return await this.waitForElementToBeVisibleAndGetText(element(by.xpath(`//label[contains(text(),\'${referenceName}\')]/following-sibling::div/sqx-references-dropdown/sqx-dropdown/span/div[@class=\'selection\']/div/span`)));
    }

    public async selectContentByText(contentBody: string) {
        await this.waitForElementToBePresent(this.$contentsItem());

        const contents = await this.$contentItems().getWebElements();

        expect(contents.length).toBeGreaterThan(0);

        for (let content of contents) {
            const text = await content.getText();

            if (text.indexOf(contentBody) >= 0) {
                await content.click();
                return;
            }
        }

        throw `No Element with contentBody ${contentBody} found`;
    }

    public async verifyCommentaryCreation() {
        await this.commentaryEditor();
        return await this.getCommentary(this.$commentaryInput());
    }

    public async verifyBoldCommentaryCreation() {
        await this.selectContentByText('Bold');
        await this.getCommentaryEditorFrame();
        return await this.getCommentary(this.$boldText());
    }


    public async verifyItalicCommentaryCreation() {
        await this.selectContentByText('Italic');
        await this.getCommentaryEditorFrame();
        return await this.getCommentary(this.$italicText());
    }


    public async verifyNumberedCommentaryCreation() {
        await this.selectContentByText('Numbered');
        await this.getCommentaryEditorFrame();
        return await this.getCommentary(await this.$numberedText());
    }

    public async verifyBulletPointsCommentaryCreation() {
        await this.selectContentByText('Bullet');
        await this.getCommentaryEditorFrame();
        return await this.getCommentary(await this.$bulletPointText());
    }

    public async clickOnNewButton() {
        await this.waitForElementToBeVisible(this.$newButton());

        // Just wait a little bit for the animation to finish.
        await browser.sleep(1000);

        return this.$newButton().click();
    }
}